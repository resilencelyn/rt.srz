// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.atl.business.configuration
{
  using System.Configuration;

  /// <summary>
  ///   The core config manager.
  /// </summary>
  public static class ConfigManager
  {
    /// <summary>
    /// The synchronization settings.
    /// </summary>
    private static SynchronizationSettings synchronizationSettings;

    /// <summary>
    /// Gets the synchronization settings.
    /// </summary>
    public static SynchronizationSettings SynchronizationSettings
    {
      get
      {
        return synchronizationSettings
               ?? (synchronizationSettings =
                   (SynchronizationSettings)ConfigurationManager.GetSection("synchronizationSettings"));
      }
    }
  }
}