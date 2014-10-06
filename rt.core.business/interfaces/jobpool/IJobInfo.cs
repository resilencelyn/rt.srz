// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IJobInfo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The JobInfo interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.jobpool
{
  using System;

  /// <summary>
  /// The JobInfo interface.
  /// </summary>
  public interface IJobInfo
  {
    #region Public Properties

    /// <summary>
    ///   Идентификатор
    /// </summary>
    Guid Id { get; }

    #endregion
  }
}