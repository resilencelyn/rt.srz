// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TFServiceRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The tf service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.TF;

  #endregion

  /// <summary>
  ///   The tf service registry.
  /// </summary>
  public class TFServiceRegistry : ServiceRegistryBase<ITFService, TFService, TFGate>
  {
  }
}