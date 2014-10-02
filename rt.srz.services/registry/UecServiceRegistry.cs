// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecServiceRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uec service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.services.Uec;
  using rt.uec.model.Interfaces;

  #endregion

  /// <summary>
  ///   The uec service registry.
  /// </summary>
  public class UecServiceRegistry : ServiceRegistryBase<IUecService, UecService, UecGate>
  {
  }
}