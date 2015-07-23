// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceRegistry.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ServiceRegistry interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  using System;

  /// <summary>
  ///   The ServiceRegistry interface.
  /// </summary>
  public interface IServiceRegistry
  {
    #region Public Methods and Operators

    /// <summary>
    /// The shutdown.
    /// </summary>
    /// <param name="span">
    /// The span.
    /// </param>
    void Shutdown(TimeSpan span);

    #endregion
  }
}