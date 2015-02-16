// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterBatchToErp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The batch erp exporter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.erp
{
  using System;
  using System.Globalization;

  using NHibernate;

  using Quartz;

  using rt.core.business.server.exchange.export;
  using rt.core.model.configuration;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.Hl7.person;
  using rt.srz.model.Hl7.person.target;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The batch erp exporter.
  /// </summary>
  public class ExporterBatchToErp : ExporterBatchSrz<PersonErp, BaseMessageTemplate>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExporterBatchToErp" /> class.
    /// </summary>
    public ExporterBatchToErp()
      : base(Exporters.ErpExporter, ExchangeSubjectType.Erz, ExchangeFileType.PersonErp)
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
        return SerializeObject.MessageCount();
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
    public override void AddNode(BaseMessageTemplate node)
    {
      SerializeObject.AddNode(node);
    }

    /// <summary>
    ///   The begin batch.
    /// </summary>
    public override void BeginBatch()
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var organisationManager = ObjectFactory.GetInstance<IOrganisationCacheManager>();
      var periodManager = ObjectFactory.GetInstance<IPeriodManager>();
      BatchId = Guid.NewGuid();
      var sender = organisationManager.GetById(SenderId);
      FileName = string.Format("{0}-{1}", sender.Okato, BatchId);

      Batch = new Batch();
      Batch.Id = BatchId;
      Batch.Subject = conceptManager.GetById(ExchangeSubjectType.Erz);
      Batch.Type = conceptManager.GetById(ExchangeFileType.PersonErp);
      Batch.Sender = sender;
      Batch.Receiver = organisationManager.GetById(ReceiverId);
      Batch.Period = periodManager.GetById(PeriodId);
      Batch.Number = 0;
      Batch.FileName = FileName;
      Batch.CodeConfirm = conceptManager.GetById(CodeConfirm.CA);
      session.Save(Batch);
      session.Flush();

      Status = StatusExportBatch.Opened;
      SerializeObject = new PersonErp { BeginPacket = GetBhs(), EndPacket = new BTS() };
    }

    /// <summary>
    /// The serialize person current.
    /// </summary>
    protected override void SerializePersonCurrent()
    {
      SerializeObject.EndPacket.CountMessages = SerializeObject.MessageCount().ToString(CultureInfo.InvariantCulture);

      // Сериализуем
      XmlSerializationHelper.SerializePersonErp(SerializeObject, GetFileNameFull());
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

    #endregion

    #region Methods

    /// <summary>
    ///   The get bhs.
    /// </summary>
    /// <returns>
    ///   The <see cref="BHS" />.
    /// </returns>
    private BHS GetBhs()
    {
      var msh = new BHS();

      // BHS.3
      msh.OriginApplicationName = new BHS3 { Application = "СРЗ " + Batch.Sender.Code };

      // BHS.4
      msh.OriginOrganizationName = new BHS4 { CodeOfRegion = Batch.Sender.Code };

      // BHS.5
      msh.ApplicationName = new BHS5 { Application = "ЦК ЕРП" };

      // BHS.6
      msh.OrganizationName = new BHS6 { FomsCode = "00" };

      // BHS.9
      msh.TypeWork = ConfigManager.ExchangeSettings.ProcesingMode;

      // BHS.10
      msh.Identificator = BatchId.ToString();

      return msh;
    }

    #endregion
  }
}