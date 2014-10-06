// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileZags.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The importer file zags.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.zags
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;

  using NHibernate;

  using Quartz;

  using rt.srz.business.manager;
  using rt.srz.model.HL7.zags;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  /// The importer file zags.
  /// </summary>
  public class ImporterFileZags : BaseImporterFileQueryResponse<Zags_VNov, DeathInfo>
  {
    #region Fields

    /// <summary>
    /// The _organization manager.
    /// </summary>
    private readonly IOrganisationManager _organizationManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ImporterFileZags"/> class.
    /// </summary>
    public ImporterFileZags()
    {
      _organizationManager = ObjectFactory.GetInstance<IOrganisationManager>();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets a value indicating whether save batch before loading xml.
    /// </summary>
    protected override bool SaveBatchBeforeLoadingXml
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Gets the type batch.
    /// </summary>
    protected override Concept TypeBatch
    {
      get
      {
        return ConceptCacheManager.GetById(TypeFile.Zags);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The applies to.
    /// </summary>
    /// <param name="file">
    /// The file.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      // 7 и 8 символы - идентификатор файла должен быть PS для данного типа файла
      return file.Name == "Zags_VNov.xml";
    }

    /// <summary>
    /// The get period code.
    /// </summary>
    /// <param name="startDate">
    /// The start date.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public int GetPeriodCode(DateTime startDate)
    {
      switch (startDate.Month)
      {
        case 1:
          return PeriodCode.PeriodCode1;
        case 2:
          return PeriodCode.PeriodCode2;
        case 3:
          return PeriodCode.PeriodCode3;
        case 4:
          return PeriodCode.PeriodCode4;
        case 5:
          return PeriodCode.PeriodCode5;
        case 6:
          return PeriodCode.PeriodCode6;
        case 7:
          return PeriodCode.PeriodCode7;
        case 8:
          return PeriodCode.PeriodCode8;
        case 9:
          return PeriodCode.PeriodCode9;
        case 10:
          return PeriodCode.PeriodCode10;
        case 11:
          return PeriodCode.PeriodCode11;
        case 12:
          return PeriodCode.PeriodCode12;
      }

      return PeriodCode.PeriodCode1;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The fill batch from zglv.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <param name="session">
    /// The session.
    /// </param>
    protected override void FillBatchFromZglv(Batch batch, Zags_VNov obj, ISession session)
    {
      batch.Number = 1; // short.Parse(obj.Zglv.Version);

      // по номеру отдела ищем загс в справочнике obj.Zglv.NumDep. и его родитель это тфомс
      // batch.Tfoms
      // batch.Sender = отделение загс
      // batch.Receiver = batch.Tfoms
      var indx = obj.Zglv.Period.IndexOf('-');
      var startDate = DateTime.ParseExact(obj.Zglv.Period.Substring(0, indx), "dd.MM.yyyy", null);

      // DateTime endDate = DateTime.ParseExact(obj.Zglv.Period.Substring(indx + 1, obj.Zglv.Period.Length - indx - 1), "dd.MM.yyyy", null);
      var periodCode = GetPeriodCode(startDate);

      var codePeriod = ConceptCacheManager.GetById(periodCode);
      var period =
        ObjectFactory.GetInstance<IPeriodManager>()
                     .GetBy(x => x.Year == startDate && x.Code == codePeriod)
                     .FirstOrDefault() ?? new Period { Year = startDate, Code = codePeriod };
      session.Save(period);
      batch.Period = period;

      batch.Subject = ConceptCacheManager.GetById(TypeSubject.Zags);
    }

    /// <summary>
    /// The get enumerable.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The <see cref="IEnumerable"/> .
    /// </returns>
    protected override IEnumerable<DeathInfo> GetEnumerable(Zags_VNov obj)
    {
      return obj.DeathInfo;
    }

    /// <summary>
    /// The get object data.
    /// </summary>
    /// <param name="xmlFilePath">
    /// The xml file path.
    /// </param>
    /// <returns>
    /// The <see cref="Zags_VNov"/>.
    /// </returns>
    protected override Zags_VNov GetObjectData(string xmlFilePath)
    {
      var zagsFactory = ObjectFactory.GetInstance<IZagsImportFactory>();
      return zagsFactory.GetImportData(xmlFilePath);
    }

    /// <summary>
    /// The get query response.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="QueryResponse"/>.
    /// </returns>
    protected override QueryResponse GetQueryResponse(DeathInfo item, Batch batch)
    {
      var result = new QueryResponse
                   {
                     InsuredPersonData =
                       new InsuredPersonDatum
                       {
                         FirstName = item.Fio.Im, 
                         LastName = item.Fio.Fam, 
                         MiddleName = item.Fio.Ot, 
                         Gender =
                           !string.IsNullOrEmpty(item.W)
                           && item.W.ToLower() == "м"
                             ? ConceptCacheManager.GetById(Sex.Sex1)
                             : ConceptCacheManager.GetById(Sex.Sex2), 
                         Birthday =
                           string.IsNullOrEmpty(item.Dr)
                             ? null
                             : (DateTime?)
                               DateTime.ParseExact(item.Dr, "dd.MM.yyyy", null), 
                         Birthplace = GetBirthPlace(item.Mr)
                       }, 
                     DeadInfo =
                       new DeadInfo
                       {
                         DateDead =
                           string.IsNullOrEmpty(item.DateDeath)
                             ? new DateTime(2200, 1, 1)
                             : DateTime.ParseExact(item.DateDeath, "dd.MM.yyyy", null), 
                         ActRecordNumber = item.NumRecord, 
                         ActRecordDate =
                           string.IsNullOrEmpty(item.DateRecord)
                             ? null
                             : (DateTime?)
                               DateTime.ParseExact(item.DateRecord, "dd.MM.yyyy", null)
                       }, 
                     Address = new address { Unstructured = GetLastLiveAddress(item.LastAddressF) }, 
                     DocumentUdl = new Document(), 
                     Message = batch.Messages.First()
                   };

      ////var foundOrg = _organizationManager.GetBy(z => z.FullName == item.OrgZags.Name_Org).FirstOrDefault();
      ////if (foundOrg != null)
      ////{
      ////  result.Organisation =  new Organisation();
      ////  result.Organisation.Id = foundOrg.Id;
      ////}
      if (item.Doc != null)
      {
        result.DocumentUdl.DateIssue = string.IsNullOrEmpty(item.Doc.DateDoc)
                                         ? null
                                         : (DateTime?)DateTime.ParseExact(item.Doc.DateDoc, "dd.MM.yyyy", null);
        if (result.DocumentUdl.DateIssue.HasValue && result.DocumentUdl.DateIssue.Value <= new DateTime(1753, 1, 1))
        {
          result.DocumentUdl.DateIssue = null;
        }

        result.DocumentUdl.Number = item.Doc.NumDoc;
        result.DocumentUdl.IssuingAuthority = item.Doc.IssuedBy;
        result.DocumentUdl.Series = item.Doc.SerDoc;
        result.DocumentUdl.DocumentType =
          ConceptCacheManager.GetBy(x => x.ShortName == item.Doc.TypeDoc && x.Oid.Id == Oid.ДокументУдл)
                             .FirstOrDefault();
      }

      return result;
    }

    /// <summary>
    /// The internal create batch.
    /// </summary>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="session">
    /// The session.
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/>.
    /// </returns>
    protected override Batch InternalCreateBatch(string fileName, ISession session)
    {
      var result = new Batch();
      result.FileName = fileName;
      return result;
    }

    /// <summary>
    /// The internal processing.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="xmlObj">
    /// The xml obj.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected override void InternalProcessing(Batch batch, Zags_VNov xmlObj, IJobExecutionContext context)
    {
      base.InternalProcessing(batch, xmlObj, context);

      // рассчет ключей
      // TODO:
      // запись в DeadInfo и проставление в инсуред персон статуса умерший
      ObjectFactory.GetInstance<IExecuteStoredManager>().ProcessZags(batch.Id);
    }

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      // todo
      return true;
    }

    /// <summary>
    /// The get birth place.
    /// </summary>
    /// <param name="birthPlace">
    /// The birth place.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetBirthPlace(MR birthPlace)
    {
      var sb = new StringBuilder();
      sb.Append(birthPlace.CountryMr)
        .Append(" ")
        .Append(birthPlace.RegionMr)
        .Append(" ")
        .Append(birthPlace.AreMr)
        .Append(" ")
        .Append(birthPlace.CityMr);
      return sb.ToString();
    }

    /// <summary>
    /// The get last live address.
    /// </summary>
    /// <param name="address">
    /// The address.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetLastLiveAddress(LastAddress address)
    {
      var sb = new StringBuilder();
      sb.Append(address.Idx)
        .Append(" ")
        .Append(address.Region)
        .Append(" ")
        .Append(address.Area)
        .Append(" ")
        .Append(address.City)
        .Append(" ")
        .Append(address.Locality)
        .Append(" ")
        .Append(address.Street)
        .Append(" ")
        .Append(address.House)
        .Append(" ")
        .Append(address.Korp)
        .Append(" ")
        .Append(address.Kv);
      return sb.ToString();
    }

    #endregion
  }
}