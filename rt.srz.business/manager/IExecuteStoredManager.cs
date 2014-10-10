// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecuteStoredManager.cs" company="��������">
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
    ///   ��������� �������� � �����
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
    /// ������� � ���� ����� � ��������� ��� �������� � ���
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
    /// ����������� ������ �����������
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
    /// ����������� ���� � ������ � ������ ��� �������
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    void ProcessZags(Guid batchId);

    #endregion
  }
}