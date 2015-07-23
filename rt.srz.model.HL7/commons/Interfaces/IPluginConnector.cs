// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPluginConnector.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The PluginConnector interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Interfaces
{
  /// <summary>
  ///   The PluginConnector interface.
  /// </summary>
  public interface IPluginConnector
  {
    #region Public Properties

    /// <summary>
    ///   Gets the id.
    /// </summary>
    string ID { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The retrieve plugin object.
    /// </summary>
    /// <returns>
    ///   The <see cref="object" />.
    /// </returns>
    object RetrievePluginObject();

    #endregion
  }
}