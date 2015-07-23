// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TEmptyStringTypes.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The t empty string types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  /// <summary>
  /// The t empty string types.
  /// </summary>
  public enum TEmptyStringTypes
  {
    /// <summary>
    /// The empty.
    /// </summary>
    Empty, // пустая строка (string.Empty)

    /// <summary>
    /// The null.
    /// </summary>
    Null, // null на месте объекта строки

    /// <summary>
    /// The signature.
    /// </summary>
    Signature, // строка, содержащая текст "null"
  }
}