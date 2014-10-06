// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileSmoRec.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file smo rec.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.smo
{
  using System;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using NLog;

  using Quartz;

  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  /// The importer file smo rec.
  /// </summary>
  public class ImporterFileSmoRec : ImporterFileSmo<RECListType, RECType>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileSmoRec"/> class.
    /// </summary>
    public ImporterFileSmoRec()
      : base(TypeSubject.Smo)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Применим ли импортер для данного типа сообщений?
    /// </summary>
    /// <param name="file">
    /// </param>
    /// <returns>
    /// true, если применим, иначе false
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      return file.Name[0] == 'k';
    }

    /// <summary>
    /// Обработка
    /// </summary>
    /// <param name="file">
    /// Путь к файлу загрузки
    /// </param>
    /// <param name="context">
    /// </param>
    /// <returns>
    /// был ли обработан пакет
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      var logger = LogManager.GetCurrentClassLogger();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // Попытка десериализации файла и создание батча
      var recList = new RECListType();
      Batch batch = null;
      try
      {
        // Десериализация
        recList = XmlSerializationHelper.Deserialize(file.FullName, recList) as RECListType;

        // Создание батча
        // batch = CreateBatch(recList);
      }
      catch (Exception ex)
      {
        // Ошибка десериализации
        logger.ErrorException(ex.Message, ex);
        throw;
      }

      // Проход по записям, маппинг и сохранение заявления
      foreach (var rec in recList.REC)
      {
        Statement statement = null;
        try
        {
          // Маппинг
          statement = MapStatement(rec);

          // Сохраняем заявление
          // ObjectFactory.GetInstance<IStatementManager>().ReplicateStatement(statement, GetPdpCodeFromFileName(batch.FileName));

          // Создаем Message
          var message = new Message();
          message.Batch = batch;
          message.IsCommit = true;
          message.IsError = false;
          message.Type = conceptManager.GetById(MessageType.K);
          ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(message);

          // Создаем MessageStatement
          var messageStat = new MessageStatement();
          messageStat.Statement = statement;
          messageStat.Message = message;
          messageStat.Version = statement.Version;
          messageStat.Type = conceptManager.GetById(MessageStatementType.MainStatement);
          ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Save(messageStat);

          // Сбрасываем изменения в БД
          ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
        }
        catch (LogicalControlException ex)
        {
          // ошибка ФЛК
          logger.InfoException(ex.Message, ex);
          logger.Info(rec);
          logger.Info(statement);
        }
        catch (Exception ex)
        {
          logger.ErrorException(ex.Message, ex);
          logger.Error(rec);
          logger.Error(statement);
        }
      }

      return true;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Создание батча
    /// </summary>
    /// <param name="recList">
    /// The rec List.
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    protected override Batch CreateBatch(RECListType recList)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var batch = new Batch();
      batch.Subject = conceptManager.GetById(TypeSubject.Smo);
      batch.Type = conceptManager.GetById(TypeFile.Rec);
      batch.FileName = recList.Filename;

      // Парсим имя файла для получения периода и номера
      DateTime? period = null;
      short number = -1;
      var splittedFileName = recList.Filename.Split(new[] { '_' });
      if (splittedFileName.Length == 3)
      {
        var month = -1;
        var strMonth = splittedFileName[2].Substring(0, 2);
        int.TryParse(strMonth, out month);

        var year = -1;
        var strYear = splittedFileName[2].Substring(2, 2);
        int.TryParse(strYear, out year);
        year += 2000;

        if (month != -1 && year != -1)
        {
          period = new DateTime(year, month, 1);
        }

        var strNumber = splittedFileName[2].Substring(4, splittedFileName[2].Length - 4);
        short.TryParse(strNumber, out number);
      }

      // Период
      if (period.HasValue)
      {
        batch.Period = ObjectFactory.GetInstance<IPeriodManager>().GetPeriodByMonth(period.Value);
      }

      // Номер
      if (number != -1)
      {
        batch.Number = number;
      }

      // Код
      batch.CodeConfirm = conceptManager.GetById(CodeConfirm.AA);

      // Получаем СМО
      var smo =
        ObjectFactory.GetInstance<IOrganisationCacheManager>()
                     .GetBy(x => x.Code == recList.Smocod && x.Oid.Id == Oid.Smo)
                     .FirstOrDefault();

      // Отправитель и получатель в даном случае один и то же ТФОМС
      if (smo != null)
      {
        batch.Sender = smo.Parent;
        batch.Receiver = smo.Parent;
      }

      // Сохраняем в базе
      session.Save(batch);
      session.Flush();

      return batch;
    }

    /// <summary>
    /// Маппинг записи из загруженного файла в заявление
    /// </summary>
    /// <param name="rec">
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    protected override Statement MapStatement(RECType rec)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var statementManager = ObjectFactory.GetInstance<IStatementManager>();

      // Получаем Statement
      var statement = statementManager.GetById(rec.Id) ?? new Statement();

      // Id
      statement.Id = rec.Id;

      // Версия
      var version = -1;
      if (!string.IsNullOrEmpty(rec.Version) && int.TryParse(rec.Version, out version))
      {
        statement.Version = version;
      }

      // Требуется выдача новго полиса
      statement.AbsentPrevPolicy = rec.NeedNewPolicy;

      // recType.IsActive = (statement.Version == statement.VersionExport && statement.IsActive) ? "1" : "0"; //TODO

      // Данные об обращении в СМО
      FillVizit(rec.Vizit, statement);

      // Данные застрахованного лица
      FillInsuredPersonData(rec.Person, statement);

      // Данные о смерти
      FillDeadInfo(rec.Person, statement);

      // Контактная информация
      FillContactInfo(rec.Person, statement);

      // Присваиваем объект в заявление
      FillRepresentative(rec.Person, statement);

      // Коды надёжности идентификации
      FillDost(rec.Person, statement);

      // Документы
      FillDocuments(rec.Doc, statement);

      // Адрес регистрации
      FillAddressG(rec.AddresG, statement);

      // Адрес проживания
      FillAddressP(rec.AddresP, statement);

      // Событие страхования
      FillInsurance(rec.Insurance, statement);

      // Медиа
      FillMediaData(rec.PersonB, statement);

      // История изменения
      FillStatementChangeData(rec.StatementChange, statement);

      return statement;
    }

    #endregion
  }
}