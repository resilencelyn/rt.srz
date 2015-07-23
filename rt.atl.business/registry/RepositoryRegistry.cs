// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр SM - инициализация всех репозиториев кроме dirty
//   Scope - синглтон
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.registry
{
  #region references

  using rt.atl.business.exchange;
  using rt.atl.business.exchange.interfaces;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр SM - инициализация всех репозиториев кроме dirty
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
             s.IncludeNamespace("rt.atl.business.manager");
             s.WithDefaultConventions().OnAddedPluginTypes(t => t.Singleton());
           });

      ForSingletonOf<IExchangeFactory>().Use<ExchangeFactory>();

      Scan(
           s =>
           {
             s.TheCallingAssembly();
             s.AddAllTypesOf<IExchange>();
           });
    }

    #endregion
  }
}