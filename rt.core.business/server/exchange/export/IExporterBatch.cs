// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExporterBatch.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ExportBatchBase interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  using System;

  using NHibernate;

  /// <summary>
  ///   The ExportBatchBase interface.
  /// </summary>
  public interface IExporterBatch
  {
    #region Public Properties

    /// <summary>
    ///   ������������� ������
    /// </summary>
    Guid BatchId { get; }

    /// <summary>
    ///   ���������� ����������� ��������� �� ������� �����
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   Gets or sets the file name.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    ///   ������������ ���������� �������
    /// </summary>
    int MaxCountBatchSession { get; set; }

    /// <summary>
    ///   ������������ ���������� ��������� � ������
    /// </summary>
    int MaxCountMessageInBatchSession { get; set; }

    /// <summary>
    ///   �����
    /// </summary>
    short Number { get; set; }

    /// <summary>
    ///   ���������� ��� ��������
    /// </summary>
    string OutDirectory { get; set; }

    /// <summary>
    ///   ������
    /// </summary>
    Guid PeriodId { get; set; }

    /// <summary>
    ///   ���������� ������
    /// </summary>
    Guid ReceiverId { get; set; }

    /// <summary>
    ///   Gets or sets the root directory.
    /// </summary>
    string RootDirectory { get; set; }

    /// <summary>
    ///   ���������� ������
    /// </summary>
    Guid SenderId { get; set; }

    /// <summary>
    ///   ������
    /// </summary>
    ISessionFactory SessionFactory { get; }

    /// <summary>
    ///   ������
    /// </summary>
    StatusExportBatch Status { get; }

    /// <summary>
    ///   Gets the type batch.
    /// </summary>
    Guid TypeBatch { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   �������� ������ ������
    /// </summary>
    void BeginBatch();

    /// <summary>
    ///   �������� ������, � ��� ���������� � (����� ��� ������)
    /// </summary>
    /// <returns> ��� �� ������ ����� </returns>
    bool CommitBatch();

    /// <summary>
    ///   ����� ������
    /// </summary>
    void UndoBatch();

    #endregion

    /// <summary>
    ///   ���������� ��� ����� ��� �������� ������
    /// </summary>
    /// <returns> ������ ��� ����� </returns>
    string GetFileNameFull();
  }
}