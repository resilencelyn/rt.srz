// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobPool.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Defines the IJobPool type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.jobpool
{
  using rt.core.business.server.jobpool;

  /// <summary>
  /// </summary>
  public interface IJobPool
  {
    #region Public Properties

    /// <summary>
    ///   Тип пула
    /// </summary>
    JobPoolType TypePool { get; }

    #endregion
  }
}