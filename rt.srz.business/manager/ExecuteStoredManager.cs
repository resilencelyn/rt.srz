// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteStoredManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The execute stored manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Configuration;

  using NHibernate;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The execute stored manager.
  /// </summary>
  public class ExecuteStoredManager : IExecuteStoredManager
  {
    #region Properties

    /// <summary>
    ///   Gets the session.
    /// </summary>
    private ISession Session
    {
      get
      {
        return ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      }
    }

    /// <summary>
    ///   Gets the time out.
    /// </summary>
    private int TimeOut
    {
      get
      {
        return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ProcExecTimeout"])
                 ? int.Parse(ConfigurationManager.AppSettings["ProcExecTimeout"])
                 : 60;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Вычисляет иерархию в КЛАДР
    /// </summary>
    public void CalculateKladrLevelAndParrentId()
    {
      var query = Session.GetNamedQuery("CalculateKladrLevelAndParrentId");
      query.SetTimeout(TimeOut);
      query.UniqueResult();
    }

    /// <summary>
    /// The calculate standard search keys.
    /// </summary>
    /// <param name="beginRecordNumber">
    /// The begin record number.
    /// </param>
    /// <param name="endRecordNumber">
    /// The end record number.
    /// </param>
    public void CalculateStandardSearchKeys(int beginRecordNumber, int endRecordNumber)
    {
      var query = Session.GetNamedQuery("CalculateStandardSearchKeys");
      query.SetTimeout(TimeOut);
      query.SetParameter("BeginRecordNumber", beginRecordNumber);
      query.SetParameter("EndRecordNumber", endRecordNumber);
      query.UniqueResult();
    }

    /// <summary>
    /// The calculate search keys exchange.
    /// </summary>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    /// <param name="beginRecordNumber">
    /// The begin record number.
    /// </param>
    /// <param name="endRecordNumber">
    /// The end record number.
    /// </param>
    public void CalculateStandardSearchKeysExchange(Guid batchId, int beginRecordNumber, int endRecordNumber)
    {
      var query = Session.GetNamedQuery("CalculateStandardSearchKeysExchange");
      query.SetTimeout(TimeOut);
      query.SetParameter("BatchId", batchId);
      query.SetParameter("BeginRecordNumber", beginRecordNumber);
      query.SetParameter("EndRecordNumber", endRecordNumber);
      query.UniqueResult();
    }

    /// <summary>
    /// The calculate search keys for statement
    /// </summary>
    /// <param name="searchKeyTypeId">
    /// The search key type id.
    /// </param>
    /// <param name="statementId">
    /// The statement Id
    /// </param>
    public void CalculateUserSearchKeyForStatement(Guid searchKeyTypeId, Guid statementId)
    {
      var query = Session.GetNamedQuery("CalculateUserSearchKeyForStatement");
      query.SetTimeout(TimeOut);
      query.SetParameter("SearchKeyTypeId", searchKeyTypeId);
      query.SetParameter("StatementId", statementId);
      query.UniqueResult();
    }

    /// <summary>
    /// The calculate user search keys.
    /// </summary>
    /// <param name="searchKeyTypeId">
    /// The search key type id.
    /// </param>
    /// <param name="beginRecordNumber">
    /// The begin record number.
    /// </param>
    /// <param name="endRecordNumber">
    /// The end record number.
    /// </param>
    public void CalculateUserSearchKeys(Guid searchKeyTypeId, int beginRecordNumber, int endRecordNumber)
    {
      var query = Session.GetNamedQuery("CalculateUserSearchKeys");
      query.SetTimeout(TimeOut);
      query.SetParameter("SearchKeyTypeId", searchKeyTypeId);
      query.SetParameter("BeginRecordNumber", beginRecordNumber);
      query.SetParameter("EndRecordNumber", endRecordNumber);
      query.UniqueResult();
    }

    /// <summary>
    /// The calculate search keys exchange.
    /// </summary>
    /// <param name="searchKeyTypeId">
    /// The search key type id.
    /// </param>
    /// <param name="batchId">
    /// The batch id.
    /// </param>
    /// <param name="beginRecordNumber">
    /// The begin record number.
    /// </param>
    /// <param name="endRecordNumber">
    /// The end record number.
    /// </param>
    public void CalculateUserSearchKeysExchange(
      Guid searchKeyTypeId, 
      Guid batchId, 
      int beginRecordNumber, 
      int endRecordNumber)
    {
      var query = Session.GetNamedQuery("CalculateSearchKeysExchange");
      query.SetTimeout(TimeOut);
      query.SetParameter("SearchKeyTypeId", searchKeyTypeId);
      query.SetParameter("BatchId", batchId);
      query.SetParameter("BeginRecordNumber", beginRecordNumber);
      query.SetParameter("EndRecordNumber", endRecordNumber);
      query.UniqueResult();
    }

    /// <summary>
    /// Создает в базе батчи и сообщения для отправки в СМО
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <param name="maxMessageCountInBatch">
    /// The max Message Count In Batch.
    /// </param>
    public void CreateExportSmoBatches(Guid periodId, int maxMessageCountInBatch)
    {
      var query = Session.GetNamedQuery("CreateExportSmoBatches");
      query.SetTimeout(TimeOut);
      query.SetParameter("PeriodId", periodId);
      query.SetParameter("MaxMessageCountInBatch", maxMessageCountInBatch);
      query.UniqueResult();
    }

    /// <summary>
    ///   The find twins.
    /// </summary>
    public void FindTwins()
    {
      var query = Session.GetNamedQuery("FindTwins");
      query.SetTimeout(TimeOut);
      query.UniqueResult();
    }

    /// <summary>
    /// Проставляет статус работающего
    /// </summary>
    /// <param name="messageId">
    /// The message Id.
    /// </param>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    public void ProcessPfr(Guid messageId, Guid periodId)
    {
      var query = Session.GetNamedQuery("ProcessPfr");
      query.SetTimeout(TimeOut);
      query.SetParameter("MessageId", messageId);
      query.SetParameter("PeriodId", periodId);
      query.UniqueResult();
    }

    /// <summary>
    /// The process snils pfr.
    /// </summary>
    /// <param name="messageId">
    /// The message Id.
    /// </param>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    public void ProcessSnilsPfr(Guid messageId, Guid periodId)
    {
      var query = Session.GetNamedQuery("ProcessSnilsPfr");
      query.SetTimeout(TimeOut);
      query.SetParameter("MessageId", messageId);
      query.SetParameter("PeriodId", periodId);
      query.UniqueResult();
    }

    /// <summary>
    /// Проставляет инфу о смерти и статус что умерший
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    public void ProcessZags(Guid batchId)
    {
      var query = Session.GetNamedQuery("ProcessZags");
      query.SetTimeout(TimeOut);
      query.SetParameter("BatchId", batchId);
      query.UniqueResult();
    }

    #endregion
  }
}