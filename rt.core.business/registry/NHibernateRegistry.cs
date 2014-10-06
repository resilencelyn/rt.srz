// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Регистрация NHibernate
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.registry
{
  #region references

  using System;
  using System.Configuration;
  using System.IO;
  using System.Linq;

  using NHibernate;
  using NHibernate.Cfg;

  using rt.core.business.nhibernate;
  using rt.core.model.configuration;

  using StructureMap.Configuration.DSL;

  using Configuration = NHibernate.Cfg.Configuration;
  using Environment = NHibernate.Cfg.Environment;

  #endregion

  /// <summary>
  ///   Регистрация NHibernate
  /// </summary>
  public class NHibernateRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="NHibernateRegistry" /> class.
    ///   Конструктор
    /// </summary>
    public NHibernateRegistry()
    {
      var configuration = new Configuration();
      configuration.Configure();
      var defaultConnectionString = ConfigurationManager.ConnectionStrings["default"];
      configuration.SetProperty(Environment.ConnectionString, defaultConnectionString.ConnectionString);
      configuration.CurrentSessionContext<SmartSessionContext>();
      var sessionFactory = configuration.BuildSessionFactory();
      ForSingletonOf<ISessionFactory>().Use(sessionFactory);

      var manager = new ManagerSessionFactorys();
      manager.SetDefaultFactory(sessionFactory);
      if (ConfigManager.PoolNhibernateSection != null)
      {
        foreach (var file in ConfigManager.PoolNhibernateSection.FileNameConfigaration.Cast<FileNameElement>())
        {
          configuration = new Configuration();
          var filename = file.FileNameConfiguration;
          if (!File.Exists(file.FileNameConfiguration))
          {
            filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file.FileNameConfiguration);
          }

          configuration.Configure(filename);
          var connectionString = ConfigurationManager.ConnectionStrings[file.FileNameConfiguration];
          configuration.SetProperty(Environment.ConnectionString, connectionString.ConnectionString);
          configuration.CurrentSessionContext<SmartSessionContext>();
          sessionFactory = configuration.BuildSessionFactory();
          manager.AddSessionFactory(file.FileNameConfiguration, sessionFactory);
        }
      }

      ForSingletonOf<IManagerSessionFactorys>().Use(manager);
    }

    #endregion
  }
}