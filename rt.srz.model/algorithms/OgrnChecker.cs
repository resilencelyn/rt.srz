// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OgrnChecker.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ogrn.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.algorithms
{
  using System;

  /// <summary>
  ///   The ogrn.
  /// </summary>
  public static class OgrnChecker
  {
    #region Static Fields

    /// <summary>
    ///   The full length.
    /// </summary>
    public static readonly byte FullLength = 13;

    #endregion

    #region Public Methods and Operators

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
      var num = id / 10L;
      var num2 = num % 11L;
      if (num2 > 9L)
      {
        num2 = num2 % 10L;
      }

      return num2 == (id - (num * 10L));
    }

    #endregion
  }
}