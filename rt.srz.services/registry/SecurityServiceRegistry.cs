// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityServiceRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The security service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Security;

  #endregion

  /// <summary>
  ///   The security service registry.
  /// </summary>
  public class SecurityServiceRegistry : ServiceRegistryBase<ISecurityService, SecurityService, SecurityGate>
  {
  }
}