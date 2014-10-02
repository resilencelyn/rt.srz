//-------------------------------------------------------------------------------------
// <copyright file="StringHelper.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//------------------------------------------------------------------------------------- 

using System;
using System.Globalization;
using System.Linq;

namespace rt.srz.model.algorithms
{
  using System.Text;

  /// <summary>
  /// The string helper.
  /// </summary>
  public static class StringHelper
  {
    /// <summary>
    /// Аппит первый символ
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ToUpperFirstChar(this string str)
    {
      return InternalToUpper(str, false);
    }

    public static string ToUpperFirstLowerOther(this string str)
    {
      return InternalToUpper(str);
    }

    private static string InternalToUpper(string str, bool otherToLower = true)
    {
      if (!string.IsNullOrEmpty(str))
      {
        string splitSymbol = " ";
        if (str.Contains("-"))
        {
          splitSymbol = "-";
        }
        var array = str.Split(char.Parse(splitSymbol));
        var sb = new StringBuilder();
        int i = 0;
        foreach (var item in array)
        {
          if (i == array.Length - 1)
          {
            splitSymbol = null;
          }
          if (item.Length > 0)
          {
            var otherstr = item.Substring(1, item.Length - 1);
            sb.Append(char.ToUpper(item[0]) + (otherToLower ? otherstr.ToLower() : otherstr) + splitSymbol);
          }
          i++;
        }

        return sb.ToString();

        ////var ch = str.First();
        ////return string.Format("{0}{1}", ch.ToString(CultureInfo.InvariantCulture).ToUpper(), str.Substring(1, str.Length - 1));
      }

      return str;
    }

    /// <summary>
    /// The crop.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <param name="length">
    /// The length.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string Crop(this string str, int length)
    {
      return string.IsNullOrEmpty(str) ? str : str.Substring(0, Math.Min(length, str.Length));
    }
  }
}
