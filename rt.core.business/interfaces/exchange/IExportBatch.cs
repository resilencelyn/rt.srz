namespace rt.core.business.interfaces.exchange
{
  using System;

  using NHibernate;

  using rt.core.business.server.exchange.export;

  /// <summary>
  /// The ExportBatchBase interface.
  /// </summary>
  public interface IExportBatch
  {
    /// <summary>
    ///   ������������� ������
    /// </summary>
    Guid BatchId { get; }

    /// <summary>
    /// ���������� ������
    /// </summary>
    Guid SenderId { get; set; }
    
    /// <summary>
    /// ���������� ������
    /// </summary>
    Guid ReceiverId { get; set; }

    /// <summary>
    /// ������
    /// </summary>
    Guid PeriodId { get; set; }

    /// <summary>
    /// �����
    /// </summary>
    short Number { get; set; }

    /// <summary>
    ///   ���������� ����������� ��������� �� ������� �����
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   ������������ ���������� �������
    /// </summary>
    int MaxCountBatchSession { get; set; }

    /// <summary>
    ///   ������������ ���������� ��������� � ������
    /// </summary>
    int MaxCountMessageInBatchSession { get; set; }

    /// <summary>
    ///   ���������� ��� ��������
    /// </summary>
    string OutDirectory { get; set; }

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
    ExportBatchType TypeBatch { get; }

    /// <summary>
    /// Gets or sets the root directory.
    /// </summary>
    string RootDirectory { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    ///  �������� ������, � ��� ���������� � (����� ��� ������)
    /// </summary>
    /// <returns> ��� �� ������ ����� </returns>
    bool CommitBatch();

    /// <summary>
    /// �������� ������ ������
    /// </summary>
    void BeginBatch();

    /// <summary>
    /// ����� ������  
    /// </summary>
    void UndoBatch();
  }
}