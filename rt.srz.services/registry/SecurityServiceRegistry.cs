// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityServiceRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The security service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.model.interfaces;
  using rt.core.services;
  using rt.core.services.registry;

  #endregion

  /// <summary>
  ///   The security service registry.
  /// </summary>
  public class SecurityServiceRegistry : ServiceRegistryBase<ISecurityService, SecurityService, SecurityGate>
  {
  }
}