// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExportSmoJobInfo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The export smo job info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  using System;

  /// <summary>
  /// The export smo job info.
  /// </summary>
  [Serializable]
  public class ExportSmoJobInfo
  {
    #region Public Properties

    /// <summary>
    ///   Идентификатор Батча
    /// </summary>
    public Guid BatchId { get; set; }

    #endregion
  }
}