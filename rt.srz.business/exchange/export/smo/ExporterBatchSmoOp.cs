// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExporterBatchSmoOp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch smo op.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.smo
{
  using System;
  using System.Collections.Generic;
  using System.Data;

  using NHibernate;

  using NLog;

  using Quartz;

  using rt.core.business.server.exchange.export;
  using rt.srz.business.configuration.algorithms.serialization;
  using rt.srz.business.manager;
  using rt.srz.business.manager.cache;
  using rt.srz.model.Hl7.smo;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The export batch smo op.
  /// </summary>
  public class ExporterBatchSmoOp : ExporterBatchSmo<OPListType, OPType>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExporterBatchSmoOp" /> class.
    /// </summary>
    public ExporterBatchSmoOp()
      : base(Exporters.SmoOpExporter, ExchangeSubjectType.Tfoms, ExchangeFileType.Op)
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
          return SerializeObject.OP.Count;
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
    public override void AddNode(OPType node)
    {
      // base.AddNode(node);
      if (node != null)
      {
        SerializeObject.OP.Add(node);
      }
    }

    /// <summary>
    ///   Создание нового пакета
    /// </summary>
    public override void BeginBatch()
    {
      Status = StatusExportBatch.Opened;

      // Создаем сериализуемй объект
      SerializeObject = new OPListType { OP = new List<OPType>(), };
    }

    /// <summary>
    /// The bulk create and export.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    public override void BulkCreateAndExport(IJobExecutionContext context, Guid batchId)
    {
      if (context.JobDetail.JobDataMap.ContainsKey("BatchId"))
      {
        BatchId = (Guid)context.JobDetail.JobDataMap["BatchId"];
      }

      // Получаем Batch
      Batch = ObjectFactory.GetInstance<IBatchManager>().GetById(BatchId);
      FileName = Batch.FileName;

      using (var session = ObjectFactory.GetInstance<ISessionFactory>().OpenStatelessSession())
      {
        var transaction = session.BeginTransaction(IsolationLevel.RepeatableRead);
        IList<StatementBatch> statementList;
        try
        {
          // Получаем записи, подлежащие обработке
          statementList =
            session.QueryOver<StatementBatch>()
                   .Where(x => x.BatchId == BatchId)
                   .RootCriteria.SetTimeout(int.MaxValue)
                   .List<StatementBatch>();
          transaction.Commit();
        }
        catch (Exception)
        {
          transaction.Dispose();
          throw;
        }

        // Стартуем батч
        BeginBatch();

        // Экспорт
        ExportStatementList(statementList, context);

        // Коммитим батч
        CommitBatch();
      }
    }

    /// <summary>
    ///   Откат пакета
    /// </summary>
    public override void UndoBatch()
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Чистим Messages
      var deleteMessagesSql = string.Format(@"delete from Message where BatchId='{0}'", BatchId);
      session.CreateSQLQuery(deleteMessagesSql).ExecuteUpdate();

      // Чистим Batch
      var deleteBatchSql = string.Format(@"delete from Batch where RowId='{0}'", BatchId);
      session.CreateSQLQuery(deleteBatchSql).ExecuteUpdate();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Сериализует текущий объект пакета
    /// </summary>
    protected override void SerializePersonCurrent()
    {
      SerializeObject.VERS = "2.1";
      SerializeObject.FILENAME = FileName;
      SerializeObject.SMOCOD = Batch.Sender.Code;
      SerializeObject.NRECORDS = Count;

      // Вычисляем код ПВП
      SerializeObject.PRZCOD = null;
      var splittedFileName = Batch.FileName.Split(new[] { '_' });
      if (splittedFileName.Length == 3)
      {
        SerializeObject.PRZCOD = splittedFileName[1];
      }

      try
      {
        // Сериализуем
        XmlSerializationHelper.SerializeToFile(SerializeObject, GetFileNameFull(), "op_list");
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

    /// <summary>
    /// The export statement list.
    /// </summary>
    /// <param name="statementList">
    /// The statement list.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    private void ExportStatementList(IList<StatementBatch> statementList, IJobExecutionContext context)
    {
      var processedCounter = 0;
      var recordCounter = 1;
      foreach (var statement in statementList)
      {
        OPType optype = null;
        var mappingWasSuccessful = true;
        try
        {
          // Маппинг 
          optype = MapOpListType(statement, recordCounter++);
        }
        catch (Exception ex)
        {
          // Логгируем 
          LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
          LogManager.GetCurrentClassLogger().Error(statement);
          mappingWasSuccessful = false;
        }

        // Удаляем не выгруженный Message из базы и переходим к следующему заявлению
        if (!mappingWasSuccessful)
        {
          // Удаляем из Message
          // var deleteMessageSql = string.Format(@"delete from Message where BatchId='{0}'", BatchId);
          // ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().CreateSQLQuery(deleteMessageSql).ExecuteUpdate();

          // var deleteMessageSql = string.Format(@"delete from Message where StatementId='{0}'", BatchId);
          // ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().CreateSQLQuery(deleteMessageSql).ExecuteUpdate();
        }

        // Добавляем в батч
        AddNode(optype);

        // Расчет прогресса
        if (context != null)
        {
          context.JobDetail.JobDataMap["progress"] = (int)(++processedCounter / (double)statementList.Count * 100);
        }
      }
    }

    #endregion
  }
}