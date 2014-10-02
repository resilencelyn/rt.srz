// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImporterFilePs.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import.pfr
{
  #region

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using Quartz;

  using rt.core.business.interfaces.exchange;
  using rt.core.business.server.exchange.export;
  using rt.srz.business.manager;
  using rt.srz.model.algorithms;
  using rt.srz.model.HL7.pfr;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The import batch ps.
  /// </summary>
  public class ImporterFilePs : BaseImporterFileQueryResponse<SnilsZlListAtr, string>
  {
    #region Fields

    /// <summary>
    ///   The exchange manager.
    /// </summary>
    private readonly IQueryResponseManager exchangeManager;

    /// <summary>
    ///   The lock object.
    /// </summary>
    private readonly object lockObject = new object();

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ImporterFilePs" /> class.
    /// </summary>
    public ImporterFilePs()
    {
      exchangeManager = ObjectFactory.GetInstance<IQueryResponseManager>();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the type batch.
    /// </summary>
    protected override Concept TypeBatch
    {
      get
      {
        return ConceptCacheManager.GetById(TypeFile.PfrSnils);
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
      // 7 и 8 символы - идентификатор файла должен быть PS для данного типа файла
      return file.Name.Substring(6, 2) == "PS";
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
    /// The <see>
    ///       <cref>IEnumerable</cref>
    ///     </see> . 
    /// </returns>
    protected override IEnumerable<string> GetEnumerable(SnilsZlListAtr obj)
    {
      return obj.Snilses;
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
    protected override QueryResponse GetQueryResponse(string item, Batch batch)
    {
      var result = new QueryResponse
        {
          Snils = SnilsChecker.SsToShort(item), 
          Feature = ConceptCacheManager.GetById(PfrFeature.PfrFeature1), 
          Message = batch.Messages.First(),
          Employment = true
        };
      return result;
    }

    /// <summary>
    /// The internal processing in transaction.
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
    protected override void InternalProcessing(Batch batch, SnilsZlListAtr xmlObj, IJobExecutionContext context)
    {
      base.InternalProcessing(batch, xmlObj, context);

      // Вызов хранимки
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      lock (lockObject)
      {
        context.JobDetail.JobDataMap["progress"] = 90;
        var transaction = session.BeginTransaction();
        try
        {
          ObjectFactory.GetInstance<IExecuteStoredManager>().ProcessSnilsPfr(batch.Messages.First().Id, batch.Period.Id);
          transaction.Commit();
          var snilses = exchangeManager.GetExportingData(batch.Id);
          if (snilses.Count > 0)
          {
            var eb =
              ObjectFactory.GetInstance<IExportBatchFactory<SnilsZlListAtr, string>>().GetExporter(ExportBatchType.Pfr);
            eb.OutDirectory = Path.Combine("Out", batch.Receiver.Oid.Id, batch.Receiver.Code);
            eb.FileName = GetFileName(batch);
            eb.BeginBatch();
            foreach (var s in snilses)
            {
              eb.AddNode(s);
            }

            eb.CommitBatch();
          }
        }
        catch (Exception)
        {
          transaction.Dispose();
          throw;
        }
      }

      context.JobDetail.JobDataMap["progress"] = 100;
    }

    /// <summary>
    /// сколько процентов от общей работы составляет обработка записей с использованием GetQueryResponse
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected override int PersentageForRecords()
    {
      return 80;
    }

    /// <summary>
    /// The get file name.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    private string GetFileName(Batch batch)
    {
      ////XXXГГКTSVVNNN.XML, где:
      ////XXX - код базового или объединенного отделения ПФР;
      ////ГГК - две последние цифры года и квартал, за который формируется файл;
      ////TS - идентификатор типа файла, неизменная часть файла;
      ////VV - порядковый номер выгрузки (версии файла);
      ////NNN - номер порции (части) выгружаемых сведений.
      return Path.GetFileNameWithoutExtension(batch.FileName.Replace("PS", "TS"));
    }

    #endregion
  }
}