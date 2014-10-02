// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolSettingsSection.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The protocol settings section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.model.protocol
{
  #region references

  using System.Configuration;

  #endregion

  /// <summary>
  ///   The protocol settings section.
  /// </summary>
  public class ProtocolSettingsSection : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    ///   Gets the protocol settings.
    /// </summary>
    [ConfigurationProperty("Settings")]
    public ProtocolSettingsCollection ProtocolSettings
    {
      get
      {
        return this["Settings"] as ProtocolSettingsCollection;
      }
    }

    #endregion
  }
}