// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The services registry.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using StructureMap.Configuration.DSL;
using al.uir.services.UIRGateContract;

namespace al.uir.services.registry
{
  #region

  

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
      ForSingletonOf<IUIRGate>().Use<UirClient>();
    }

    #endregion
  }
}