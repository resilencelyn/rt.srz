// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MigratorConfiguration.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Defines the MigratorConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.configuration
{
  using System.Configuration;

  /// <summary>
  /// The migrator configuration.
  /// </summary>
  public class MigratorConfiguration : ConfigurationSection
  {
    /// <summary>
    ///   Версия миграции
    /// </summary>
    [ConfigurationProperty("MigrationVersion", DefaultValue = -1, IsRequired = false)]
    public int MigrationVersion
    {
      get { return (int)this["MigrationVersion"]; }
      set { this["MigrationVersion"] = value; }
    }

    /// <summary>
    /// Gets or sets the provider name.
    /// </summary>
    [ConfigurationProperty("ProviderName")]
    public string ProviderName
    {
      get { return (string)this["ProviderName"]; }
      set { this["ProviderName"] = value; }
    }
  }
}
