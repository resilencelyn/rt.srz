// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The core config manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.configuration
{
  using System.Configuration;

  /// <summary>
  ///   The core config manager.
  /// </summary>
  public static class ConfigManager
  {
    #region Static Fields

    /// <summary>
    ///   The pool nhibernate section.
    /// </summary>
    private static ExchangeSettings exchangeSettings;

    /// <summary>
    ///   The migrator configuration.
    /// </summary>
    private static MigratorConfiguration migratorConfiguration;

    /// <summary>
    ///   The pool nhibernate section.
    /// </summary>
    private static PoolNhibernateSection poolNhibernateSection;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the exchange settings.
    /// </summary>
    public static ExchangeSettings ExchangeSettings
    {
      get
      {
        return exchangeSettings
               ?? (exchangeSettings = (ExchangeSettings)ConfigurationManager.GetSection("exchangeSettings"));
      }
    }

    /// <summary>
    ///   Gets the migrator configuration.
    /// </summary>
    public static MigratorConfiguration MigratorConfiguration
    {
      get
      {
        return migratorConfiguration
               ?? (migratorConfiguration = (MigratorConfiguration)ConfigurationManager.GetSection("migration"));
      }
    }

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

    #endregion
  }
}