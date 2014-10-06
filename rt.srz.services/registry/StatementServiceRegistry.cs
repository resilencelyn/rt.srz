// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementServiceRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement service registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.registry
{
  #region

  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.services.Statement;

  #endregion

  /// <summary>
  ///   The statement service registry.
  /// </summary>
  public class StatementServiceRegistry : ServiceRegistryBase<IStatementService, StatementService, StatementGate>
  {
  }
}