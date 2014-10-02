// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExchangeSettings.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Настройки импорта/экспорта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Configuration;

using rt.core.business.nhibernate.target;

#endregion

namespace rt.core.business.configuration
{
  using rt.core.business.configuration.target;

  /// <summary>
  ///   Настройки импорта/экспорта
  /// </summary>
  public class ExchangeSettings : ConfigurationSection
  {
    /// <summary>
    ///   WorkingFolderExchange
    /// </summary>
    [ConfigurationProperty("WorkingFolderExchange", IsRequired = false)]
    public string WorkingFolderExchange
    {
      get { return (string)this["WorkingFolderExchange"]; }
      set { this["WorkingFolderExchange"] = value; }
    }

    /// <summary>
    ///   Режим отправки сообщений (трасинг, дебаг)
    /// </summary>
    [ConfigurationProperty("ProcesingMode", IsRequired = false)]
    public string ProcesingMode
    {
      get { return (string)this["ProcesingMode"]; }
      set { this["ProcesingMode"] = value; }
    }

    /// <summary>
    ///   Путь по умолчанию для успешно обработанных файлов
    /// </summary>
    [ConfigurationProperty("ProcessedPath", IsRequired = true)]
    public string ProcessedPath
    {
      get { return this["ProcessedPath"] as string; }
    }

    /// <summary>
    ///   Путь по умолчанию для файлов в результате обработки которых произошла ошибка
    /// </summary>
    [ConfigurationProperty("FailedPath", IsRequired = true)]
    public string FailedPath
    {
      get { return this["FailedPath"] as string; }
    }

    /// <summary>
    ///  Сколько потоков можно запускать при импорте
    /// </summary>
    [ConfigurationProperty("CountThreadImportFiles", IsRequired = false)]
    public int CountThreadImportFiles
    {
      get { return (int)this["CountThreadImportFiles"]; }
      set { this["CountThreadImportFiles"] = value; }
    }

    /// <summary>
    /// Gets the directory watch collection.
    /// </summary>
    [ConfigurationProperty("DirectoryWatchConfiguration")]
    public DirectoryWatchCollection DirectoryWatchCollection
    {
      get
      {
        return base["DirectoryWatchConfiguration"] as DirectoryWatchCollection;
      }
    }

    /// <summary>
    /// Папка с результатом бэкапа
    /// </summary>
    [ConfigurationProperty("BackupOutputFolder", IsRequired = true)]
    public string BackupOutputFolder
    {
      get { return (string)this["BackupOutputFolder"]; }
      set { this["BackupOutputFolder"] = value; }
    }

    /// <summary>
    /// Отключать ли пользователей от работы
    /// </summary>
    [ConfigurationProperty("DisconnectUsers", IsRequired = false)]
    public bool DisconnectUsers
    {
      get { return (bool)this["DisconnectUsers"]; }
      set { this["DisconnectUsers"] = value; }
    }

    /// <summary>
    ///   Gets a value indicating whether the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only.
    /// </summary>
    /// <returns> true if the <see cref="T:System.Configuration.ConfigurationElement" /> object is read-only; otherwise, false. </returns>
    public override bool IsReadOnly()
    {
      return false;
    }
  }
}