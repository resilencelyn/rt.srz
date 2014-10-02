// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using CrystalQuartz.Core.SchedulerProviders;
using Quartz;
using Quartz.Impl;
using rt.core.business.configuration;
using rt.core.business.nhibernate;
using rt.core.business.quartz;
using StructureMap.Configuration.DSL;

#endregion

namespace rt.core.business.registry
{

  /// <summary>
  ///   The core registry.
  /// </summary>
  public class CoreRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CoreRegistry" /> class.
    /// </summary>
    public CoreRegistry()
    {
      // Синглтоны
      ForSingletonOf<INHibernateSession>().Use<NHibernateSession>();
    }

    #endregion
  }
}