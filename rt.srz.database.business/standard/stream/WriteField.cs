// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WriteField.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.stream
{
  using System.IO;

  /// <summary>
  /// The write field.
  /// </summary>
  public abstract class WriteField : IWriteField
  {
    #region IWriteField Members

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

 public class WriteFieldImpl : WriteField
  {
   public override void Write(BinaryWriter writer, string value)
   {
     throw new System.NotImplementedException();
   }
  }
}