// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmoServiceRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The smo service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Smo;

  #endregion

  /// <summary>
  ///   The smo service registry.
  /// </summary>
  public class SmoServiceRegistry : ServiceRegistryBase<ISmoService, SmoService, SmoGate>
  {
  }
}