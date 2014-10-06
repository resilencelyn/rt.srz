// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWatcher.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Watcher interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.directorywatcher
{
  /// <summary>
  ///   The Watcher interface.
  /// </summary>
  public interface IWatcher
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets a value indicating whether enable raising events.
    /// </summary>
    bool EnableRaisingEvents { get; set; }

    /// <summary>
    ///   Gets or sets the filter.
    /// </summary>
    string Filter { get; set; }

    /// <summary>
    ///   Gets or sets the path.
    /// </summary>
    string Path { get; set; }

    #endregion
  }
}