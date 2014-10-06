// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The core registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.registry
{
  using rt.core.business.nhibernate;
  using rt.core.business.security.interfaces;
  using rt.core.business.security.repository;

  using StructureMap.Configuration.DSL;

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

      ForSingletonOf<ISecurityProvider>().Use<SecurityProvider>();

      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.IgnoreStructureMapAttributes();

             ////s.ExcludeNamespace("");
             s.IncludeNamespace("rt.core.business.manager");
             s.WithDefaultConventions().OnAddedPluginTypes(t => t.Singleton());
           });
    }

    #endregion
  }
}