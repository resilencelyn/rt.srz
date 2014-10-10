// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NsiServiceRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Regulatory;

  #endregion

  /// <summary>
  ///   The nsi service registry.
  /// </summary>
  internal class NsiServiceRegistry : ServiceRegistryBase<IRegulatoryService, RegulatoryService, RegulatoryGate>
  {
  }
}