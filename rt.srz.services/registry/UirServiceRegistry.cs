// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirServiceRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Uir;

  /// <summary>
  ///   The uir service registry.
  /// </summary>
  public class UirServiceRegistry : ServiceRegistryBase<IUirService, UirService, UirGate>
  {
  }
}