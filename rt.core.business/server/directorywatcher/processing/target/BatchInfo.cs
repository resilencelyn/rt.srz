// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchInfo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.core.business.server.directorywatcher.processing.target
{
  using System.IO;
  using System.Threading.Tasks;

  /// <summary>
  ///   The batch info.
  /// </summary>
  public class BatchInfo
  {
    /// <summary>
    /// Gets or sets the file info.
    /// </summary>
    public FileInfo FileInfo { get; set; }

    /// <summary>
    /// Gets or sets the priority.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Gets or sets the thread.
    /// </summary>
    public Task Task { get; set; }
  }
}