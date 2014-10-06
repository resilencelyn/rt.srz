// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceClientRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр SM - инициализация всех репозиториев
//   Scope - синглтон
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.config
{
  #region references

  using rt.atl.model.interfaces.Service;
  using rt.core.model.security;
  using rt.srz.model.interfaces.service;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр SM - инициализация всех репозиториев
  ///   Scope - синглтон
  /// </summary>
  public class ServiceClientRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceClientRegistry" /> class.
    ///   Создает новый экземпляр <see cref="ServiceClientRegistry" />.
    /// </summary>
    public ServiceClientRegistry()
    {
      ForSingletonOf<IAtlService>().Use<AtlClient>();
      ForSingletonOf<IStatementService>().Use<StatementClient>();
      ForSingletonOf<IAuthService>().Use<AuthClient>();

      ForSingletonOf<IUirService>().Use<UirClient>();
    }

    #endregion
  }
}