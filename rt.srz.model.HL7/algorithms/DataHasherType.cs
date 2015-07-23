// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataHasherType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The data hasher type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.algorithms
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The data hasher type.
  /// </summary>
  [CLSCompliant(false)]
  public enum DataHasherType : byte
  {
    /// <summary>
    ///   The cr c 32 b.
    /// </summary>
    CRC32b = 0
  }
}