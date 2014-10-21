// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр SM - инициализация всех репозиториев
//   Scope - синглтон
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.business.registry
{
  #region references

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр SM - инициализация всех репозиториев
  ///   Scope - синглтон
  /// </summary>
  public class RepositoryRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="RepositoryRegistry" /> class.
    ///   Создает новый экземпляр <see cref="RepositoryRegistry" />.
    /// </summary>
    public RepositoryRegistry()
    {
      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.IgnoreStructureMapAttributes();

             ////s.ExcludeNamespace("");
             s.IncludeNamespace("rt.fias.business.manager");
             s.WithDefaultConventions().OnAddedPluginTypes(t => t.Singleton());
           });
    }

    #endregion
  }
}