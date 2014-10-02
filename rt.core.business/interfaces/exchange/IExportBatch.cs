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
    ///   Идентификатор пакета
    /// </summary>
    Guid BatchId { get; }

    /// <summary>
    /// Получатель пакета
    /// </summary>
    Guid SenderId { get; set; }
    
    /// <summary>
    /// Получатель пакета
    /// </summary>
    Guid ReceiverId { get; set; }

    /// <summary>
    /// Период
    /// </summary>
    Guid PeriodId { get; set; }

    /// <summary>
    /// Номер
    /// </summary>
    short Number { get; set; }

    /// <summary>
    ///   Количество выгруженных сообщений за текущий сеанс
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   Максимальное количество пакетов
    /// </summary>
    int MaxCountBatchSession { get; set; }

    /// <summary>
    ///   Максимальное количество сообщений в пакете
    /// </summary>
    int MaxCountMessageInBatchSession { get; set; }

    /// <summary>
    ///   Директория для выгрузки
    /// </summary>
    string OutDirectory { get; set; }

    /// <summary>
    ///   Сессия
    /// </summary>
    ISessionFactory SessionFactory { get; }

    /// <summary>
    ///   Статус
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
    ///  Закрытие пакета, и его сохранение в (файле или памяти)
    /// </summary>
    /// <returns> был ли закрыт пакет </returns>
    bool CommitBatch();

    /// <summary>
    /// Создание нового пакета
    /// </summary>
    void BeginBatch();

    /// <summary>
    /// Откат пакета  
    /// </summary>
    void UndoBatch();
  }
}