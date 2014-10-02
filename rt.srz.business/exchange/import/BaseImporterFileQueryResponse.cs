// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseImporterFileQueryResponse.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.import
{
  #region

  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;

  using NHibernate;

  using NLog;

  using Quartz;

  using rt.core.business.server.exchange.import;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  /// The base import batch pfr.
  /// </summary>
  /// <typeparam name="TXmlObj">
  /// Тип xml модели входного файла 
  /// </typeparam>
  /// <typeparam name="TEnumerableItem">
  /// Тип элемента массива 
  /// </typeparam>
  public abstract class BaseImporterFileQueryResponse<TXmlObj, TEnumerableItem> : ImporterFile
  {
    #region Constants

    /// <summary>
    ///   через какое количество обработанных данных (запись таблицы) делать flush
    /// </summary>
    protected const int CountPerFlush = 500;

    #endregion

    #region Fields

    /// <summary>
    ///   The concept cache manager.
    /// </summary>
    protected readonly IConceptCacheManager ConceptCacheManager;

    /// <summary>
    ///   The _fom manager.
    /// </summary>
    protected readonly IOrganisationManager FomsManager;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="BaseImporterFileQueryResponse{TXmlObj,TEnumerableItem}" /> class.
    /// </summary>
    protected BaseImporterFileQueryResponse()
      : base(TypeSubject.Pfr)
    {
      ConceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      FomsManager = ObjectFactory.GetInstance<IOrganisationManager>();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets a value indicating whether save batch before loading xml.
    /// </summary>
    protected virtual bool SaveBatchBeforeLoadingXml
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Gets the type batch.
    /// </summary>
    protected virtual Concept TypeBatch
    {
      get
      {
        return null;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Обработка
    /// </summary>
    /// <param name="file">
    /// Путь к файлу загрузки 
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <returns>
    /// был ли обработан пакет 
    /// </returns>
    public override bool Processing(FileInfo file, IJobExecutionContext context)
    {
      try
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var batch = CreateBatch(file.Name);
        try
        {
          var xmlObj = GetObjectData(file.FullName);

          FillBatchFromZglv(batch, xmlObj, session);

          if (!SaveBatchBeforeLoadingXml)
          {
            session.Save(batch);
            session.Flush();
          }

          InternalProcessing(batch, xmlObj, context);
          return true;
        }
        catch (Exception ex)
        {
          LogManager.GetCurrentClassLogger().ErrorException("Ошибка загрузки файла ПФР", ex);
          UndoBatch(batch.Id);
          throw;
        }
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().ErrorException("Ошибка загрузки файла", ex);
        throw;
      }
    }

    /// <summary>
    /// The undo batches.
    /// </summary>
    /// <param name="fileName">
    /// The file name. 
    /// </param>
    public override void UndoBatches(string fileName)
    {
      var batches = ObjectFactory.GetInstance<IBatchManager>().GetBy(z => z.FileName == fileName);
      foreach (var batch in batches)
      {
        UndoBatch(batch.Id);
      }
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
    protected virtual void FillBatchFromZglv(Batch batch, TXmlObj obj, ISession session)
    {
    }

    /// <summary>
    /// The get enumerable.
    /// </summary>
    /// <param name="obj">
    /// The obj. 
    /// </param>
    /// <returns>
    /// The <see>
    ///                 <cref>IEnumerable</cref>
    ///               </see> . 
    /// </returns>
    protected abstract IEnumerable<TEnumerableItem> GetEnumerable(TXmlObj obj);

    /// <summary>
    /// The get object data.
    /// </summary>
    /// <param name="xmlFilePath">
    /// The xml file path. 
    /// </param>
    /// <returns>
    /// The <see cref="TXmlObj"/> . 
    /// </returns>
    protected virtual TXmlObj GetObjectData(string xmlFilePath)
    {
      TXmlObj xmlObj;
      var serializer = XmlSerializationHelper.GetSerializer(typeof(TXmlObj));
      using (var fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read))
      {
        xmlObj = (TXmlObj)serializer.Deserialize(fs);
      }

      return xmlObj;
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
    protected abstract QueryResponse GetQueryResponse(TEnumerableItem item, Batch batch);

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
    /// The <see cref="Batch"/> . 
    /// </returns>
    protected virtual Batch InternalCreateBatch(string fileName, ISession session)
    {
      var codeOpfr = fileName.Substring(0, 3);
      var batch = new Batch();
      batch.FileName = fileName;
      batch.Number = GetFileVersion(fileName);
      batch.Subject = ConceptCacheManager.GetById(TypeSubject.Pfr);
      var opfr = FomsManager.GetTfomByOpfrCode(codeOpfr);
      batch.Sender = opfr;
      batch.Receiver = opfr.Parent;

      var year = 2000 + int.Parse(fileName.Substring(3, 2));
      var quarter = int.Parse(fileName.Substring(5, 1));
      var periodCode = -1;
      switch (quarter)
      {
        case 1:
          periodCode = PeriodCode.PeriodCode21;
          break;
        case 2:
          periodCode = PeriodCode.PeriodCode22;
          break;
        case 3:
          periodCode = PeriodCode.PeriodCode23;
          break;
        case 4:
          periodCode = PeriodCode.PeriodCode24;
          break;
      }

      var y = new DateTime(year, 1, 1);
      var codePeriod = ConceptCacheManager.GetById(periodCode);
      var period =
        ObjectFactory.GetInstance<IPeriodManager>().GetBy(x => x.Year == y && x.Code == codePeriod).FirstOrDefault()
        ?? new Period { Year = y, Code = codePeriod };
      session.Save(period);
      batch.Period = period;

      return batch;
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
    protected virtual void InternalProcessing(Batch batch, TXmlObj xmlObj, IJobExecutionContext context)
    {
      var sessionFactory = ObjectFactory.GetInstance<ISessionFactory>();
      var session = sessionFactory.GetCurrentSession();
      var countProcessed = 0;
      var all = GetEnumerable(xmlObj).Count();
      using (var statelessSession = sessionFactory.OpenStatelessSession())
      {
        var items = GetEnumerable(xmlObj).ToList();
        statelessSession.SetBatchSize(1);
        foreach (var item in items)
        {
          InsertQueryOver(batch, statelessSession, item);
          countProcessed++;
          context.JobDetail.JobDataMap["progress"] = (countProcessed * PersentageForRecords()) / all;
        }

        statelessSession.Close();
      }

      context.JobDetail.JobDataMap["progress"] = 80;
    }

    /// <summary>
    /// The insert query over.
    /// </summary>
    /// <param name="batch">
    /// The batch.
    /// </param>
    /// <param name="statelessSession">
    /// The stateless session.
    /// </param>
    /// <param name="item">
    /// The item.
    /// </param>
    private void InsertQueryOver(Batch batch, IStatelessSession statelessSession, TEnumerableItem item)
    {
      var queryResponse = GetQueryResponse(item, batch);

      if (queryResponse.InsuredPersonData != null)
      {
        statelessSession.Insert(queryResponse.InsuredPersonData);
      }

      if (queryResponse.DocumentUdl != null)
      {
        statelessSession.Insert(queryResponse.DocumentUdl);
      }

      if (queryResponse.Address != null)
      {
        statelessSession.Insert(queryResponse.Address);
      }

      statelessSession.Insert(queryResponse);
    }

    /// <summary>
    ///   сколько процентов от общей работы составляет обработка записей с использованием GetQueryResponse
    /// </summary>
    /// <returns> The <see cref="int" /> . </returns>
    protected virtual int PersentageForRecords()
    {
      return 80;
    }

    /// <summary>
    /// The undo batch.
    /// </summary>
    /// <param name="batch">
    /// The batch. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    protected override bool UndoBatch(Guid batch)
    {
      try
      {
        var session = SessionFactory.GetCurrentSession();
        session.Flush();

        session.CreateSQLQuery(
          @"delete from EmploymentHistory where QueryResponseId in (select qr.RowId from QueryResponse qr inner join Message m on m.RowId = qr.MessageId where BatchId = :BatchId)")
          .SetParameter("BatchId", batch).SetTimeout(10800).UniqueResult();

        session.CreateSQLQuery(
          @"delete from SearchKey where QueryResponseId in (select qr.RowId from QueryResponse qr inner join Message m on m.RowId = qr.MessageId where BatchId = :BatchId)")
          .SetParameter("BatchId", batch).SetTimeout(10800).UniqueResult();

        session.CreateSQLQuery(
          @"delete from QueryResponse where MessageId in (select RowId from Message where BatchId = :BatchId)")
          .SetParameter("BatchId", batch).SetTimeout(10800).UniqueResult();

        session.CreateSQLQuery(@"delete from Message where BatchId = :BatchId").SetParameter("BatchId", batch)
          .SetTimeout(10800).UniqueResult();

        ObjectFactory.GetInstance<IBatchManager>().Delete(x => x.Id == batch);
        session.Flush();

        return true;
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().Error(ex);
        return false;
      }
    }

    /// <summary>
    /// The create batch.
    /// </summary>
    /// <param name="fileName">
    /// The file name. 
    /// </param>
    /// <returns>
    /// The <see cref="Batch"/> . 
    /// </returns>
    private Batch CreateBatch(string fileName)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var batch = InternalCreateBatch(fileName, session);
      batch.CodeConfirm = ConceptCacheManager.GetById(CodeConfirm.AA);
      batch.Type = TypeBatch;

      if (SaveBatchBeforeLoadingXml)
      {
        session.Save(batch);
        var message = new Message { Batch = batch, IsCommit = true, IsError = false, Type = batch.Type };
        session.Save(message);
        session.Flush();
        session.Refresh(batch);
      }

      return batch;
    }

    /// <summary>
    /// The get file version.
    /// </summary>
    /// <param name="fileName">
    /// The file name. 
    /// </param>
    /// <returns>
    /// The <see cref="short"/> . 
    /// </returns>
    private short GetFileVersion(string fileName)
    {
      // XXXГГКRSVVNNN.XML, где:
      // XXX - код базового или объединенного отделения ПФР;
      // ГГК - две последние цифры года и квартал, за который формируется файл;
      // RS - идентификатор типа файла, неизменная часть файла;
      // VV - порядковый номер выгрузки (версии файла);
      // NNN - номер порции (части) выгружаемых сведений.
      return short.Parse(fileName.Substring(8, 2));
    }

    #endregion
  }
}