// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   регистр SM - инициализация всех репозиториев
//   Scope - синглтон
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.registry
{
  #region references

  using rt.atl.model.interfaces.Service;
  using rt.core.model.interfaces;
  using rt.core.model.security;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.client.services;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   регистр SM - инициализация всех репозиториев
  ///   Scope - синглтон
  /// </summary>
  public class ServicesRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServicesRegistry" /> class.
    ///   Создает новый экземпляр <see cref="ServicesRegistry" />.
    /// </summary>
    public ServicesRegistry()
    {
      ForSingletonOf<IAuthService>().Use<AuthClient>();
      ForSingletonOf<ISecurityService>().Use<SecurityClient>();

      ForSingletonOf<IAtlService>().Use<AtlClient>();

      ForSingletonOf<IAddressService>().Use<AddressClient>();
      ForSingletonOf<IRegulatoryService>().Use<RegulatoryClient>();
      ForSingletonOf<IStatementService>().Use<StatementClient>();
      ForSingletonOf<ITfomsService>().Use<TfomsClient>();
    }

    #endregion
  }
}