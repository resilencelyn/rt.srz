// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The core config manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;

namespace rt.atl.model.configuration
{
  /// <summary>
  ///   The core config manager.
  /// </summary>
  public static class ConfigManager
  {
    #region Static Fields

    /// <summary>
    ///   The synchronization settings.
    /// </summary>
    private static SynchronizationSettings synchronizationSettings;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the synchronization settings.
    /// </summary>
    public static SynchronizationSettings SynchronizationSettings
    {
      get
      {
        return synchronizationSettings ?? (synchronizationSettings = (SynchronizationSettings)ConfigurationManager.GetSection("synchronizationSettings"));
      }
    }

    #endregion
  }
}