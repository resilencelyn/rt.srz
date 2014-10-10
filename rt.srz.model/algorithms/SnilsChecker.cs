// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnilsChecker.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The snils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.algorithms
{
  using System;

  using rt.srz.model.Hl7.dotNetX;

  /// <summary>
  ///   The snils.
  /// </summary>
  public static class SnilsChecker
  {
    #region Static Fields

    /// <summary>
    ///   The full length.
    /// </summary>
    public static readonly byte FullLength = 11;

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
      id = SsToShort(id);
      if (id.Length != FullLength)
      {
        return false;
      }

      try
      {
        return CheckIdentifier(Convert.ToUInt64(id));
      }
      catch
      {
        return false;
      }
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
      if (id > 0xf4a0eL)
      {
        byte minDigits = 11;
        var buffer = NumbersHelper.SplitNumber(id, minDigits);
        var num = ((((((((9 * buffer[0]) + (8 * buffer[1])) + (7 * buffer[2])) + (6 * buffer[3])) + (5 * buffer[4]))
                     + (4 * buffer[5])) + (3 * buffer[6])) + (2 * buffer[7])) + buffer[8];
        while (num > 0x65)
        {
          num = num % 0x65;
        }

        switch (num)
        {
          case 100:
          case 0x65:
            num = 0;
            break;
        }

        if (num != ((10 * buffer[9]) + buffer[10]))
        {
          return false;
        }
      }

      return true;
    }

    /// <summary>
    /// The ss to long.
    /// </summary>
    /// <param name="ss">
    /// The ss.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string SsToLong(string ss)
    {
      return !string.IsNullOrEmpty(ss) && ss.Length == 11
               ? string.Format(
                               "{0}-{1}-{2} {3}", 
                               ss.Substring(0, 3), 
                               ss.Substring(3, 3), 
                               ss.Substring(6, 3), 
                               ss.Substring(9, 2))
               : string.Empty;
    }

    /// <summary>
    /// The ss to short.
    /// </summary>
    /// <param name="ss">
    /// The ss.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string SsToShort(string ss)
    {
      return ss.Replace(" ", string.Empty).Replace("-", string.Empty).Replace("_", string.Empty);
    }

    #endregion
  }
}