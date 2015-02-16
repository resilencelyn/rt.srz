// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExporterBatch.cs" company="РусБИТех">
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
    ///   Идентификатор пакета
    /// </summary>
    Guid BatchId { get; }

    /// <summary>
    ///   Количество выгруженных сообщений за текущий сеанс
    /// </summary>
    int Count { get; }

    /// <summary>
    ///   Gets or sets the file name.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    ///   Максимальное количество пакетов
    /// </summary>
    int MaxCountBatchSession { get; set; }

    /// <summary>
    ///   Максимальное количество сообщений в пакете
    /// </summary>
    int MaxCountMessageInBatchSession { get; set; }

    /// <summary>
    ///   Номер
    /// </summary>
    short Number { get; set; }

    /// <summary>
    ///   Директория для выгрузки
    /// </summary>
    string OutDirectory { get; set; }

    /// <summary>
    ///   Период
    /// </summary>
    Guid PeriodId { get; set; }

    /// <summary>
    ///   Получатель пакета
    /// </summary>
    Guid ReceiverId { get; set; }

    /// <summary>
    ///   Gets or sets the root directory.
    /// </summary>
    string RootDirectory { get; set; }

    /// <summary>
    ///   Получатель пакета
    /// </summary>
    Guid SenderId { get; set; }

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
    Guid TypeBatch { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Создание нового пакета
    /// </summary>
    void BeginBatch();

    /// <summary>
    ///   Закрытие пакета, и его сохранение в (файле или памяти)
    /// </summary>
    /// <returns> был ли закрыт пакет </returns>
    bool CommitBatch();

    /// <summary>
    ///   Откат пакета
    /// </summary>
    void UndoBatch();

    #endregion

    /// <summary>
    ///   Возвращает имя файла для текущего пакета
    /// </summary>
    /// <returns> Полное имя файла </returns>
    string GetFileNameFull();
  }
}