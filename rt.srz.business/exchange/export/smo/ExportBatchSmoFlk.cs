// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchSmoFlk.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch smo flk.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.smo
{
  using System;
  using System.Collections.Generic;

  using NHibernate;

  using NLog;

  using rt.core.business.server.exchange.export;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.Hl7.smo;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The export batch smo flk.
  /// </summary>
  public class ExportBatchSmoFlk : ExportBatchSmo<PFLKType, PRType>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExportBatchSmoFlk" /> class.
    /// </summary>
    public ExportBatchSmoFlk()
      : base(ExportBatchType.SmoFlk, TypeSubject.Tfoms, TypeFile.Flk)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Количество выгруженных сообщений за текущий сеанс
    /// </summary>
    public override int Count
    {
      get
      {
        if (SerializeObject != null)
        {
          return SerializeObject.PR.Count;
        }

        return 0;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add node.
    /// </summary>
    /// <param name="node">
    /// The node.
    /// </param>
    public override void AddNode(PRType node)
    {
      if (node != null)
      {
        SerializeObject.PR.Add(node);
      }
    }

    /// <summary>
    ///   Создание нового пакета
    /// </summary>
    public override void BeginBatch()
    {
      // Создаем батч в БД
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var organisationManager = ObjectFactory.GetInstance<IOrganisationCacheManager>();
      var periodManager = ObjectFactory.GetInstance<IPeriodManager>();
      Batch = new Batch();
      Batch.Subject = conceptManager.GetById(TypeSubject.Smo);
      Batch.Type = conceptManager.GetById(TypeFile.Flk);
      Batch.Sender = organisationManager.GetById(SenderId);
      Batch.Receiver = organisationManager.GetById(ReceiverId);
      Batch.Period = periodManager.GetById(PeriodId);
      Batch.Number = Number;
      Batch.FileName = FileName;
      Batch.CodeConfirm = conceptManager.GetById(CodeConfirm.CA);
      session.Save(Batch);
      session.Flush();
      BatchId = Batch.Id;

      Status = StatusExportBatch.Opened;

      // Создаем сериализуемй объект
      SerializeObject = new PFLKType { PR = new List<PRType>() };
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Сериализует текущий объект пакета
    /// </summary>
    protected override void SerializePersonCurrent()
    {
      SerializeObject.VERS = "2.1";
      SerializeObject.FNAME = FileName;
      SerializeObject.FNAME_I = FileName.Replace("f", "i"); // Меняем префикс в имени файла ответа

      try
      {
        // Сериализуем
        XmlSerializationHelper.SerializeToFile(SerializeObject, GetFileNameFull(), "pflk_list");
        base.SerializePersonCurrent();

        // Пишем в базу код успешной выгрзуки
        var batch = ObjectFactory.GetInstance<IBatchManager>().GetById(BatchId);
        if (batch != null)
        {
          batch.CodeConfirm = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(CodeConfirm.AA);
          ObjectFactory.GetInstance<IBatchManager>().SaveOrUpdate(batch);
          ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
        }
      }
      catch (Exception ex)
      {
        // Ошибка сериализации
        // Логгируем ошибку
        LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
        throw;
      }
    }

    #endregion
  }
}