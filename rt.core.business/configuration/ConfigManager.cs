// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Configuration;
using rt.core.business.nhibernate;

#endregion

namespace rt.core.business.configuration
{
  /// <summary>
  ///   The core config manager.
  /// </summary>
  public static class ConfigManager
  {
    /// <summary>
    ///   The pool nhibernate section.
    /// </summary>
    private static PoolNhibernateSection poolNhibernateSection;

    /// <summary>
    ///   The pool nhibernate section.
    /// </summary>
    private static ExchangeSettings exchangeSettings;

    /// <summary>
    /// The migrator configuration.
    /// </summary>
    private static MigratorConfiguration migratorConfiguration;

    /// <summary>
    ///   Gets the pool nhibernate section.
    /// </summary>
    public static PoolNhibernateSection PoolNhibernateSection
    {
      get
      {
        return poolNhibernateSection
               ?? (poolNhibernateSection =
                   (PoolNhibernateSection)ConfigurationManager.GetSection("PoolNhibernateSection"));
      }
    }

    /// <summary>
    /// Gets the migrator configuration.
    /// </summary>
    public static MigratorConfiguration MigratorConfiguration
    {
      get
      {
        return migratorConfiguration
             ?? (migratorConfiguration =
                 (MigratorConfiguration)ConfigurationManager.GetSection("migration"));
      }
    }

    /// <summary>
    /// Gets the exchange settings.
    /// </summary>
    public static ExchangeSettings ExchangeSettings
    {
      get
      {
        return exchangeSettings
               ?? (exchangeSettings =
                   (ExchangeSettings)ConfigurationManager.GetSection("exchangeSettings"));
      }
    }
  }
}