// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportBatchSmoRec.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export batch pfr.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.exchange.export.smo
{
  #region

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
  using rt.srz.model.HL7.smo;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The export batch pfr.
  /// </summary>
  public class ExportBatchSmoRec : ExportBatchSmo<RECListType, RECType>
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ExportBatchSmoRec" /> class.
    /// </summary>
    public ExportBatchSmoRec()
      : base(ExportBatchType.SmoRec, TypeSubject.Smo, TypeFile.Rec)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   ���������� ����������� ��������� �� ������� �����
    /// </summary>
    public override int Count
    {
      get
      {
        if (SerializeObject != null)
        {
          return SerializeObject.REC.Count;
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
    public override void AddNode(RECType node)
    {
      // base.AddNode(node);
      if (node != null)
      {
        SerializeObject.REC.Add(node);
      }
    }

    /// <summary>
    ///   �������� ������ ������
    /// </summary>
    public override void BeginBatch()
    {
      Status = StatusExportBatch.Opened;

      // ������� ������������ ������
      SerializeObject = new RECListType { REC = new List<RECType>(), };
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
      BatchId = batchId;

      Batch = ObjectFactory.GetInstance<IBatchManager>().GetById(BatchId);
      FileName = Batch.FileName;

      using (var session = ObjectFactory.GetInstance<ISessionFactory>().OpenStatelessSession())
      {
        var transaction = session.BeginTransaction(IsolationLevel.RepeatableRead);
        IList<StatementBatch> statementList;
        try
        {
          // �������� ������, ���������� ���������
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

        // �������� ����
        BeginBatch();

        // �������
        ExportStatementList(statementList, context);

        // �������� ����
        CommitBatch();
      }
    }

    /// <summary>
    ///   ����� ������
    /// </summary>
    public override void UndoBatch()
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // ������ Messages
      var deleteMessagesSql = string.Format(@"delete from Message where BatchId='{0}'", BatchId);
      session.CreateSQLQuery(deleteMessagesSql).ExecuteUpdate();

      // ������ Batch
      var deleteBatchSql = string.Format(@"delete from Batch where RowId='{0}'", BatchId);
      session.CreateSQLQuery(deleteBatchSql).ExecuteUpdate();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   ����������� ������� ������ ������
    /// </summary>
    protected override void SerializePersonCurrent()
    {
      SerializeObject.Vers = "2.1";
      SerializeObject.Filename = FileName;
      SerializeObject.Smocod = Batch.Receiver.Code;
      SerializeObject.Nrecords = Count;

      try
      {
        // �����������
        XmlSerializationHelper.SerializeToFile(SerializeObject, GetFileNameFull(), "rec_list");
        base.SerializePersonCurrent();

        // ����� � ���� ��� �������� ��������
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
        // ������ ������������
        // ��������� ������
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
      foreach (var statement in statementList)
      {
        RECType recType = null;
        var mappingWasSuccessful = true;
        try
        {
          // ������� 
          recType = MapRecListType(statement);
        }
        catch (Exception ex)
        {
          // ��������� 
          LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
          LogManager.GetCurrentClassLogger().Error(statement);
          mappingWasSuccessful = false;
        }

        // ������� �� ����������� Message �� ���� � ��������� � ���������� ���������
        if (!mappingWasSuccessful)
        {
          // ������� �� Message
          // var deleteMessageSql = string.Format(@"delete from Message where BatchId='{0}'", BatchId);
          // ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().CreateSQLQuery(deleteMessageSql).ExecuteUpdate();

          // var deleteMessageSql = string.Format(@"delete from Message where StatementId='{0}'", BatchId);
          // ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().CreateSQLQuery(deleteMessageSql).ExecuteUpdate();
        }

        // ��������� � ����
        AddNode(recType);

        // ������ ���������
        if (context != null)
        {
          context.JobDetail.JobDataMap["progress"] = (int)(++processedCounter / (double)statementList.Count * 100);
        }
      }
    }

    #endregion
  }
}