// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobNames.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The job names.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.quartz
{
  /// <summary>
  /// The job names.
  /// </summary>
  public class JobNames
  {
    #region Constants

    /// <summary>
    ///   Константа
    /// </summary>
    public const string BackupDatabase = "Бэкап базы данных";

    /// <summary>
    ///   Константа
    /// </summary>
    public const string InitializationOfListeners = "Инициализация прослушивателей файловой системы";

    /// <summary>
    ///   Константа
    /// </summary>
    public const string PacketLoading = "Загрузка пакетов";

    #endregion
  }
}