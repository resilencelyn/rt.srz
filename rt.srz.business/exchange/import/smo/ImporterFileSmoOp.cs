// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileSmoOp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file smo op.
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

  using rt.core.business.interfaces.exchange;
  using rt.core.business.server.exchange.export;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.HL7.smo;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  /// The importer file smo op.
  /// </summary>
  public class ImporterFileSmoOp : ImporterFileSmo<OPListType, OPType>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileSmoOp"/> class.
    /// </summary>
    public ImporterFileSmoOp()
      : base(TypeSubject.Tfoms)
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
      return file.Name[0] == 'i';
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
      var opList = new OPListType();
      Batch batch = null;
      try
      {
        // Десериализация
        opList = XmlSerializationHelper.Deserialize(file.FullName, opList) as OPListType;

        // Создание батча
        batch = CreateBatch(opList);
      }
      catch (Exception ex)
      {
        // Ошибка десериализации либо создания бачта
        logger.Error(ex.Message, ex);
        throw;
      }

      // Вычисляем код ПВП
      var pdpCode = string.Empty;
      var splittedFileName = batch.FileName.Split(new[] { '_' });
      if (splittedFileName.Length == 3)
      {
        pdpCode = splittedFileName[1];
      }

      // Создаем экспортер для файлов ответа и стартуем батч
      var repExp =
        ObjectFactory.GetInstance<IExportBatchFactory<REPListType, REPType>>().GetExporter(ExportBatchType.SmoRep);
      repExp.FileName = batch.FileName.Replace("i", "p"); // Меняем префикс в имени файла ответа
      repExp.Number = batch.Number;
      repExp.PeriodId = batch.Period.Id;
      repExp.SenderId = batch.Receiver.Id;
      repExp.ReceiverId = batch.Sender.Id;
      repExp.BeginBatch();

      // Создаем экспортер для файлов ответа c протоколом ФЛК и стартуем батч
      var flkRepExp =
        ObjectFactory.GetInstance<IExportBatchFactory<PFLKType, PRType>>().GetExporter(ExportBatchType.SmoFlk);
      flkRepExp.FileName = batch.FileName.Replace("i", "f"); // Меняем префикс в имени файла ответа
      flkRepExp.Number = batch.Number;
      flkRepExp.PeriodId = batch.Period.Id;
      flkRepExp.SenderId = batch.Receiver.Id;
      flkRepExp.ReceiverId = batch.Sender.Id;
      flkRepExp.BeginBatch();

      // Проход по записям, маппинг и сохранение заявления
      var goodAnswer = true;
      foreach (var op in opList.OP)
      {
        Statement statement = null;
        try
        {
          // Маппинг
          statement = MapStatement(op);

          // Сохраняем заявление

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

          goodAnswer = true;
        }
        catch (LogicalControlException ex)
        {
          // ошибка ФЛК
          logger.Info(ex.Message, ex);
          logger.Info(op);
          logger.Info(statement);
          goodAnswer = false;

          // Пишем ошибки ФЛК в ответ
          foreach (var err in ex.LogicalControlExceptions)
          {
            // Пишем ошибки ФЛК в ответ
            var flkAnswer = new PRType();
            flkAnswer.N_REC = op.N_REC;
            flkAnswer.OSHIB = err.Info.Code;
            flkAnswer.COMMENT = err.Message;
            flkRepExp.AddNode(flkAnswer);
          }
        }
        catch (Exception ex)
        {
          logger.Error(ex.Message, ex);
          logger.Error(op);
          logger.Error(statement);
          goodAnswer = false;
        }

        // Пишем ответ
        var answer = new REPType();
        answer.N_REC = op.N_REC;
        answer.ID = op.ID;
        answer.CODE_ERP = goodAnswer ? CodeConfirm.AA : CodeConfirm.CA;
        repExp.AddNode(answer);
      }

      // Коммитим батч ответа
      repExp.CommitBatch();

      // Коммитим батч ответа ФЛК
      flkRepExp.CommitBatch();

      return true;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Создание батча
    /// </summary>
    /// <param name="opList">
    /// The op List.
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    protected override Batch CreateBatch(OPListType opList)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var batch = new Batch();
      batch.Subject = conceptManager.GetById(TypeSubject.Tfoms);
      batch.Type = conceptManager.GetById(TypeFile.Op);
      batch.FileName = opList.FILENAME;

      // Парсим имя файла для получения периода и номера
      DateTime? period = null;
      short number = -1;
      var splittedFileName = opList.FILENAME.Split(new[] { '_' });
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
                     .GetBy(x => x.Code == opList.SMOCOD && x.Oid.Id == Oid.Smo)
                     .FirstOrDefault();

      if (smo != null)
      {
        batch.Sender = smo;
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
    /// <param name="op">
    /// The op.
    /// </param>
    /// <returns>
    /// The <see cref="Statement"/>.
    /// </returns>
    protected override Statement MapStatement(OPType op)
    {
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var statementManager = ObjectFactory.GetInstance<IStatementManager>();

      // Получаем Statement
      var statement = statementManager.GetById(op.ID) ?? new Statement();

      // Id
      statement.Id = op.ID;

      // Версия
      var version = -1;
      if (!string.IsNullOrEmpty(op.VERSION) && int.TryParse(op.VERSION, out version))
      {
        statement.Version = version;
      }

      // Требуется выдача новго полиса
      statement.AbsentPrevPolicy = op.NEED_NEW_POLICY;

      // recType.IsActive = (statement.Version == statement.VersionExport && statement.IsActive) ? "1" : "0"; //TODO

      // Данные об обращении в СМО
      FillVizit(op.VIZIT, statement);

      // Данные застрахованного лица
      FillInsuredPersonData(op.PERSON, statement);

      // Данные о смерти
      FillDeadInfo(op.PERSON, statement);

      // Контактная информация
      FillContactInfo(op.PERSON, statement);

      // Присваиваем объект в заявление
      FillRepresentative(op.PERSON, statement);

      // Коды надёжности идентификации
      FillDost(op.PERSON, statement);

      // Документы
      FillDocuments(op.DOC_LIST, statement);

      // Адрес регистрации
      FillAddressG(op.ADDRES_G, statement);

      // Адрес проживания
      FillAddressP(op.ADDRES_P, statement);

      // Событие страхования
      FillInsurance(op.INSURANCE, statement);

      // Медиа
      FillMediaData(op.PERSONB, statement);

      // История изменения
      FillStatementChangeData(op.STATEMENT_CHANGE, statement);

      return statement;
    }

    #endregion
  }
}