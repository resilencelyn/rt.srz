// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecuteStoredManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
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
    /// 
    /// </summary>
    /// <param name="beginRecordNumber"></param>
    /// <param name="endRecordNumber"></param>
    void CalculateStandardSearchKeys(int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="batchId"></param>
    /// <param name="beginRecordNumber"></param>
    /// <param name="endRecordNumber"></param>
    void CalculateStandardSearchKeysExchange(Guid batchId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchKeyTypeId"></param>
    /// <param name="beginRecordNumber"></param>
    /// <param name="endRecordNumber"></param>
    void CalculateUserSearchKeys(Guid searchKeyTypeId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchKeyTypeId"></param>
    /// <param name="batchId"></param>
    /// <param name="beginRecordNumber"></param>
    /// <param name="endRecordNumber"></param>
    void CalculateUserSearchKeysExchange(Guid searchKeyTypeId, Guid batchId, int beginRecordNumber, int endRecordNumber);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchKeyTypeId"></param>
    /// <param name="statementId"></param>
    void CalculateUserSearchKeyForStatement(Guid searchKeyTypeId, Guid statementId);

    /// <summary>
    /// The find twins.
    /// </summary>
    void FindTwins();

    /// <summary>
    /// The process snils pfr.
    /// </summary>
    /// <param name="messageId"> </param>
    /// <param name="periodId"> </param>
    void ProcessSnilsPfr(Guid messageId, Guid periodId);

    /// <summary>
    /// Проставляет статус работающего
    /// </summary>
    /// <param name="messageId"> </param>
    /// <param name="periodId"> </param>
    void ProcessPfr(Guid messageId, Guid periodId);

    /// <summary>
    /// Проставляет инфу о смерти и статус что умерший
    /// </summary>
    /// <param name="batchId"></param>
    void ProcessZags(Guid batchId);

    /// <summary>
    /// Вычисляет иерархию в КЛАДР
    /// </summary>
    void CalculateKladrLevelAndParrentId();

    /// <summary>
    /// Создает в базе батчи и сообщения для отправки в СМО
    /// </summary>
    /// <param name="periodId"></param>
    /// <param name="maxMessageCountInBatch"></param>
    void CreateExportSmoBatches(Guid periodId, int maxMessageCountInBatch);

    #endregion
  }
}