// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMessageFactory.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request
{
  /// <summary>
  /// The MessageFactory interface.
  /// </summary>
  public interface IMessageFactory
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get exporter.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <returns>
    /// The <see cref="IMessageExporter"/>.
    /// </returns>
    IMessageExporter GetExporter(int type);

    #endregion
  }
}