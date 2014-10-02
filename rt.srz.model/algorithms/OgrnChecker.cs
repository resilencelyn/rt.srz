// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OgrnChecker.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The ogrn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;

#endregion

namespace rt.srz.model.algorithms
{
  /// <summary>
  ///   The ogrn.
  /// </summary>
  public static class OgrnChecker
  {
    /// <summary>
    ///   The full length.
    /// </summary>
    public static readonly byte FullLength = 13;

    /// <summary>
    /// The check identifier.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckIdentifier(string id)
    {
      if (id.Length != FullLength)
      {
        return false;
      }

      return CheckIdentifier(Convert.ToUInt64(id));
    }

    /// <summary>
    /// The check identifier.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static bool CheckIdentifier(ulong id)
    {
      var num = id/10L;
      var num2 = num%11L;
      if (num2 > 9L)
      {
        num2 = num2%10L;
      }

      return num2 == (id - (num*10L));
    }
  }
}