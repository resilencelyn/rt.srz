// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrServiceRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The kladr service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Kladr;

  #endregion

  /// <summary>
  ///   The kladr service registry.
  /// </summary>
  public class KladrServiceRegistry : ServiceRegistryBase<IKladrService, KladrService, KladrGate>
  {
  }
}