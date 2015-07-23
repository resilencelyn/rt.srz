// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteField.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The write field.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.stream
{
  using System;
  using System.IO;

  /// <summary>
  ///   The write field.
  /// </summary>
  public abstract class WriteField : IWriteField
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
    public abstract void Write(BinaryWriter writer, string value);

    #endregion
  }

  /// <summary>
  /// The write field impl.
  /// </summary>
  public class WriteFieldImpl : WriteField
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
    /// <exception cref="NotImplementedException">
    /// </exception>
    public override void Write(BinaryWriter writer, string value)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}