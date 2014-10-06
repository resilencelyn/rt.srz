// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceRegistryManager.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The host manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  using System.Collections.Generic;

  using rt.core.model.interfaces;

  /// <summary>
  ///   The host manager.
  /// </summary>
  public class ServiceRegistryManager
  {
    #region Static Fields

    /// <summary>
    /// The service hosts.
    /// </summary>
    public static List<IServiceRegistry> ServiceHosts = new List<IServiceRegistry>();

    #endregion
  }
}