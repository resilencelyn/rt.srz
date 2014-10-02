// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuartzRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.registry
{
  #region

  using System.Collections.Specialized;
  using System.Configuration;
  using Quartz;
  using Quartz.Impl;

  using rt.core.business.server.exchange.import;

  using StructureMap.Configuration.DSL;
  using CrystalQuartz.Core.SchedulerProviders;

  using rt.core.business.interfaces.directorywatcher;
  using rt.core.business.quartz;
  using rt.core.business.interfaces.exchange;
  using rt.core.business.interfaces.quartz;

  #endregion

  /// <summary>
  /// The quartz registry.
  /// </summary>
  public class QuartzRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="QuartzRegistry"/> class.
    /// </summary>
    public QuartzRegistry()
    {
      // Синглтоны
      ForSingletonOf<ISchedulerProvider>().Use<SchedulerProvider>();
      ForSingletonOf<IImporterFileFactory>().Use<ImporterFileFactory>();

      // Sheduler
      RegistryScheduller();
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The register scheduller.
    /// </summary>
    private void RegistryScheduller()
    {
      ForSingletonOf<ISchedulerFactory>().Use(new StdSchedulerFactory());
      ForSingletonOf<IQuartzInfoProvider>().Use(new QuartzInfoProvider());
    }

    #endregion
  }
}