// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchInfo.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The batch info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.server.directorywatcher.processing.target
{
  using System.IO;
  using System.Threading.Tasks;

  /// <summary>
  ///   The batch info.
  /// </summary>
  public class BatchInfo
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the file info.
    /// </summary>
    public FileInfo FileInfo { get; set; }

    /// <summary>
    ///   Gets or sets the priority.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///   Gets or sets the thread.
    /// </summary>
    public Task Task { get; set; }

    #endregion
  }
}