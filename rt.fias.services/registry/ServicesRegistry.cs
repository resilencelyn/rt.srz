// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The services registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.services.registry
{
  #region

  using rt.core.model.interfaces;

  using StructureMap.Configuration.DSL;

  #endregion

  /// <summary>
  ///   The services registry.
  /// </summary>
  public class ServicesRegistry : Registry
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServicesRegistry" /> class.
    /// </summary>
    public ServicesRegistry()
    {
      ForSingletonOf<IAddressService>().Use<FiasGateInternal>();
    }

    #endregion
  }
}