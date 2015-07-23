// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigratorConfiguration.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The migrator configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.configuration
{
  using System.Configuration;

  /// <summary>
  ///   The migrator configuration.
  /// </summary>
  public class MigratorConfiguration : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    ///   Версия миграции
    /// </summary>
    [ConfigurationProperty("MigrationVersion", DefaultValue = -1, IsRequired = false)]
    public int MigrationVersion
    {
      get
      {
        return (int)this["MigrationVersion"];
      }

      set
      {
        this["MigrationVersion"] = value;
      }
    }

    /// <summary>
    ///   Gets or sets the provider name.
    /// </summary>
    [ConfigurationProperty("ProviderName")]
    public string ProviderName
    {
      get
      {
        return (string)this["ProviderName"];
      }

      set
      {
        this["ProviderName"] = value;
      }
    }

    #endregion
  }
}