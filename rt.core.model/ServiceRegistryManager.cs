// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceRegistryManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  using System.Collections.Generic;

  /// <summary>
  ///   The host manager.
  /// </summary>
  public class ServiceRegistryManager
  {
    public static List<IServiceRegistry> ServiceHosts = new List<IServiceRegistry>();
  }
}