// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFileRs.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The import batch .
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.pfr
{
  #region

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  using Quartz;

  using rt.srz.business.manager;
  using rt.srz.model.algorithms;
  using rt.srz.model.Hl7.pfr;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The import batch .
  /// </summary>
  public class ImporterFileRs : BaseImporterFileQueryResponse<ZlList, Zl>
  {
    #region Properties

    /// <summary>
    ///   Gets the type batch.
    /// </summary>
    protected override Concept TypeBatch
    {
      get
      {
        return ConceptCacheManager.GetById(ExchangeFileType.PfrData);
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
    /// The <see cref="bool"/> .
    /// </returns>
    public override bool AppliesTo(FileInfo file)
    {
      // 7 и 8 символы - идентификатор файла должен быть RS для данного типа файла
      return file.Name.Substring(6, 2) == "RS";
    }

    #endregion

    #region Methods

    /// <summary>
    /// The get enumerable.
    /// </summary>
    /// <param name="obj">
    /// The obj.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IEnumerable</cref>
    ///   </see>
    ///   .
    /// </returns>
    protected override IEnumerable<Zl> GetEnumerable(ZlList obj)
    {
      return obj.Zl;
    }

    /// <summary>
    /// The get pfr exchange.
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
    protected override QueryResponse GetQueryResponse(Zl item, Batch batch)
    {
      var result = new QueryResponse
                   {
                     Message = batch.Messages.First(), 
                     Snils = SnilsChecker.SsToShort(item.Snils), 
                     Feature = GetFeature(item), 
                     InsuredPersonData =
                       new InsuredPersonDatum
                       {
                         Snils = SnilsChecker.SsToShort(item.Snils), 
                         LastName = item.Fam, 
                         FirstName = item.Im, 
                         MiddleName = item.Ot, 
                         Birthday =
                           string.IsNullOrEmpty(item.Dr)
                             ? null
                             : (DateTime?)
                               DateTime.ParseExact(item.Dr, "dd.MM.yyyy", null), 
                         BirthdayType = int.Parse(item.Dostdr), 
                         Birthplace = item.AddressR, 
                         Gender =
                           !string.IsNullOrEmpty(item.W)
                           && item.W.ToLower() == "м"
                             ? ConceptCacheManager.GetById(Sex.Sex1)
                             : ConceptCacheManager.GetById(Sex.Sex2), 
                       }, 
                     Address = new address { Postcode = item.Index, Unstructured = item.AddressReg }, 
                     DocumentUdl = new Document(), 
                     Employment = item.IdZl == "1"
                   };

      if (item.Doc != null)
      {
        result.DocumentUdl.DateIssue = string.IsNullOrEmpty(item.Doc.DateDoc)
                                         ? null
                                         : (DateTime?)DateTime.ParseExact(item.Doc.DateDoc, "dd.MM.yyyy", null);
        if (result.DocumentUdl.DateIssue.HasValue && result.DocumentUdl.DateIssue.Value <= new DateTime(1753, 1, 1))
        {
          result.DocumentUdl.DateIssue = null;
        }

        result.DocumentUdl.Number = item.Doc.NDoc;
        result.DocumentUdl.Series = item.Doc.SDoc;
        result.DocumentUdl.DocumentType =
          ConceptCacheManager.GetBy(x => x.ShortName == item.Doc.NameDoc && x.Oid.Id == Oid.ДокументУдл)
                             .FirstOrDefault();
      }

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
    protected override void InternalProcessing(Batch batch, ZlList xmlObj, IJobExecutionContext context)
    {
      base.InternalProcessing(batch, xmlObj, context);
      var manager = ObjectFactory.GetInstance<IExecuteStoredManager>();

      // Рассчёт стандартных ключей
      manager.CalculateStandardSearchKeysExchange(batch.Id, 1, xmlObj.Zl.Count);

      // Расчет пользовательских ключей
      var searchKeyTypes =
        ObjectFactory.GetInstance<ISearchKeyTypeManager>()
                     .GetBy(
                            x =>
                            x.Tfoms.Id == batch.Receiver.Id && x.IsActive
                            && x.OperationKey.Id == OperationKey.FullScanAndSaveKey);
      foreach (var keyType in searchKeyTypes)
      {
        manager.CalculateUserSearchKeysExchange(keyType.Id, batch.Id, 1, xmlObj.Zl.Count);
      }

      // замена снилсов и проставление что работающий (EmployementHistory)
      manager.ProcessPfr(batch.Messages.First().Id, batch.Period.Id);
      context.JobDetail.JobDataMap["progress"] = 100;
    }

    /// <summary>
    ///   сколько процентов от общей работы составляет обработка записей с использованием GetQueryResponse
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    protected override int PersentageForRecords()
    {
      return 80;
    }

    /// <summary>
    /// The get feature.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    /// <returns>
    /// The <see cref="Concept"/> .
    /// </returns>
    private Concept GetFeature(Zl item)
    {
      switch (item.Pi)
      {
        case "0":
          return null;
        case "1":
          return ConceptCacheManager.GetById(PfrFeature.PfrFeature1);
        case "2":
          return ConceptCacheManager.GetById(PfrFeature.PfrFeature2);
        case "3":
          return ConceptCacheManager.GetById(PfrFeature.PfrFeature3);
      }

      return null;
    }

    #endregion
  }
}