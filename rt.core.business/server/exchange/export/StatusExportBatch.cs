// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusExportBatch.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Статус экспортера пакетов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.exchange.export
{
  /// <summary>
  ///   Статус экспортера пакетов
  /// </summary>
  public enum StatusExportBatch
  {
    /// <summary>
    ///   Не инициализированый
    /// </summary>
    NotInit = 0, 

    /// <summary>
    ///   Открыт
    /// </summary>
    Opened = 1, 

    /// <summary>
    ///   Подтвержден
    /// </summary>
    Commited
  }
}