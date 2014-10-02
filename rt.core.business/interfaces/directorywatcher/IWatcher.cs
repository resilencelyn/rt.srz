// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWatcher.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  /// <summary>
  ///   The Watcher interface.
  /// </summary>
  public interface IWatcher
  {
    /// <summary>
    ///   Gets or sets the filter.
    /// </summary>
    string Filter { get; set; }

    /// <summary>
    ///   Gets or sets the path.
    /// </summary>
    string Path { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether enable raising events.
    /// </summary>
    bool EnableRaisingEvents { get; set; }
  }
}