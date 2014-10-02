// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SynchronizationSettings.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Настройки импорта/экспорта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.configuration
{
  using System.Configuration;

  /// <summary>
  ///   Настройки импорта/экспорта
  /// </summary>
  public class SynchronizationSettings : ConfigurationSection
  {
    /// <summary>
    ///   WorkingFolderExchange
    /// </summary>
    [ConfigurationProperty("SynchronizationNsi", IsRequired = true)]
    public bool SynchronizationNsi
    {
      get { return (bool)this["SynchronizationNsi"]; }
      set { this["SynchronizationNsi"] = value; }
    }

    /// <summary>
    ///   Режим отправки сообщений (трасинг, дебаг)
    /// </summary>
    [ConfigurationProperty("SynchronizationToSrz", IsRequired = true)]
    public bool SynchronizationToSrz
    {
      get { return (bool)this["SynchronizationToSrz"]; }
      set { this["SynchronizationToSrz"] = value; }
    }

    /// <summary>
    ///   Режим отправки сообщений (трасинг, дебаг)
    /// </summary>
    [ConfigurationProperty("SynchronizationToPvp", IsRequired = true)]
    public bool SynchronizationToPvp
    {
      get { return (bool)this["SynchronizationToPvp"]; }
      set { this["SynchronizationToPvp"] = value; 
      }
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