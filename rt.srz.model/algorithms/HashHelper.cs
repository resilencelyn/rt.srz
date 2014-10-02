// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The hash helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Security.Cryptography;

#endregion

namespace rt.srz.model.algorithms
{
  using rt.srz.model.HL7.algorithms.DamienG;

  /// <summary>
  ///   The hash helper.
  /// </summary>
  public static class HashHelper
  {
    /// <summary>
    ///   The debug pseudo hasher name.
    /// </summary>
    public static readonly string DebugPseudoHasherName = "DamienG.CRC32b";

    /// <summary>
    ///   The default policy hasher name.
    /// </summary>
    public static readonly string DefaultPolicyHasherName = "GOST3411";

    /// <summary>
    /// The create hash algorithm.
    /// </summary>
    /// <param name="hasherName">
    /// The hasher name.
    /// </param>
    /// <returns>
    /// The <see cref="HashAlgorithm"/>.
    /// </returns>
    public static HashAlgorithm CreateHashAlgorithm(string hasherName)
    {
      if (string.Compare(hasherName, DebugPseudoHasherName, StringComparison.Ordinal) == 0)
      {
        return new Crc32();
      }

      return HashAlgorithm.Create(hasherName);
    }

    /// <summary>
    /// The hash algorithms equal.
    /// </summary>
    /// <param name="algorithm1">
    /// The algorithm 1.
    /// </param>
    /// <param name="algorithm2">
    /// The algorithm 2.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool HashAlgorithmsEqual(string algorithm1, string algorithm2)
    {
      return string.Compare(algorithm1, algorithm2, StringComparison.OrdinalIgnoreCase) == 0;
    }
  }
}