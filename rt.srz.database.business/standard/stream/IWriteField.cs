// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriteField.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The WriteField interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.stream
{
  using System.IO;

  /// <summary>
  ///   The WriteField interface.
  /// </summary>
  public interface IWriteField
  {
    #region Public Methods and Operators

    /// <summary>
    /// The write.
    /// </summary>
    /// <param name="writer">
    /// The writer.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    void Write(BinaryWriter writer, string value);

    #endregion
  }
}