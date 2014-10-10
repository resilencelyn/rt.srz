// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecuteStoredManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExecuteStoredManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;

  #endregion

  /// <summary>
  ///   The ExecuteStoredManager interface.
  /// </summary>
  public interface IExecuteStoredManager
  {
    #region Public Methods and Operators

    /// <summary>
    ///   Вычисляет иерархию в КЛАДР
    /// </summary>
    void CalculateKladrLevelAndParrentId();

    /// <summary>
    /// The calculate standard search keys.
    /// </summary>
    /// <param name="beginRecordNumber">
    /// The begin record number.
    /// </param>
    /// <param name="endRecordNumber">
    /// The end record number.
    /// </param>
    void CalculateStandardSearchKeys(int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// The calculate standard search keys exchange.
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
    void CalculateStandardSearchKeysExchange(Guid batchId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// The calculate user search key for statement.
    /// </summary>
    /// <param name="searchKeyTypeId">
    /// The search key type id.
    /// </param>
    /// <param name="statementId">
    /// The statement id.
    /// </param>
    void CalculateUserSearchKeyForStatement(Guid searchKeyTypeId, Guid statementId);

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
    void CalculateUserSearchKeys(Guid searchKeyTypeId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// The calculate user search keys exchange.
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
    void CalculateUserSearchKeysExchange(Guid searchKeyTypeId, Guid batchId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// Создает в базе батчи и сообщения для отправки в СМО
    /// </summary>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    /// <param name="maxMessageCountInBatch">
    /// The max Message Count In Batch.
    /// </param>
    void CreateExportSmoBatches(Guid periodId, int maxMessageCountInBatch);

    /// <summary>
    ///   The find twins.
    /// </summary>
    void FindTwins();

    /// <summary>
    /// Проставляет статус работающего
    /// </summary>
    /// <param name="messageId">
    /// The message Id.
    /// </param>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    void ProcessPfr(Guid messageId, Guid periodId);

    /// <summary>
    /// The process snils pfr.
    /// </summary>
    /// <param name="messageId">
    /// The message Id.
    /// </param>
    /// <param name="periodId">
    /// The period Id.
    /// </param>
    void ProcessSnilsPfr(Guid messageId, Guid periodId);

    /// <summary>
    /// Проставляет инфу о смерти и статус что умерший
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    void ProcessZags(Guid batchId);

    #endregion
  }
}