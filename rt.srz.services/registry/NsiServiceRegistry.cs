﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NsiServiceRegistry.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  using rt.srz.services.NSI;

  #endregion

  /// <summary>
  ///   The nsi service registry.
  /// </summary>
  internal class NsiServiceRegistry : ServiceRegistryBase<INsiService, NsiService, NsiGate>
  {
  }
}