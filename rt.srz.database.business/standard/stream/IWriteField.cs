// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWriteField.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.stream
{
  using System.IO;

  /// <summary>
  ///   The WriteField interface.
  /// </summary>
  public interface IWriteField
  {
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
  }
}