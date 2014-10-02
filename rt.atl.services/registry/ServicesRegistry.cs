//-----------------------------------------------------------------------
// <copyright file="ServicesRegistry.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------    

namespace rt.atl.services.registry
{
  using rt.atl.model.interfaces.Service;

  using StructureMap.Configuration.DSL;

  /// <summary>
  /// The services registry.
  /// </summary>
  public class ServicesRegistry : Registry
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServicesRegistry"/> class.
    /// </summary>
    public ServicesRegistry()
    {
      
    }
  }
}
