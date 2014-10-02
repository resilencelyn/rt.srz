// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The string extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

#endregion

namespace rt.srz.barcode.converter
{
  /// <summary>
  ///   The string extensions.
  /// </summary>
  public static class StringExtensions
  {
    #region Static Fields

    /// <summary>
    ///   The hex symbols.
    /// </summary>
    private static readonly List<char> HexSymbols =
      new List<char>(
        new[]
          {
            'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
          });

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The from base 64 string.
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] FromBase64String(this string str)
    {
      return Convert.FromBase64String(str);
    }

    /// <summary>
    /// The from hex string.
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] FromHexString(this string str)
    {
      if (str == null)
      {
        return null;
      }

      str = str.Replace(" ", string.Empty);

      if ((str.Length%2) > 0)
      {
        throw new Exception("Wrong argument specified. String length must be even.");
      }

      var charArray = str.ToCharArray();
      var buffer = new byte[charArray.Length/2];
      for (var i = 0; i < charArray.Length; i += 2)
      {
        if (!HexSymbols.Contains(charArray[i]) || !HexSymbols.Contains(charArray[i + 1]))
        {
          throw new Exception("Bad symbol found in hexidecimal string value [" + charArray[i] + charArray[i + 1]);
        }

        buffer[i/2] =
          byte.Parse(
            charArray[i].ToString(CultureInfo.InvariantCulture)
            + charArray[i + 1].ToString(CultureInfo.InvariantCulture), 
            NumberStyles.HexNumber);
      }

      return buffer;
    }

    /// <summary>
    /// The get bytes.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="codePage">
    /// The code page. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] GetBytes(this string value, [Optional] [DefaultParameterValue(0x4e3)] int codePage)
    {
      var encoding = Encoding.GetEncoding(codePage);
      return !string.IsNullOrEmpty(value) ? encoding.GetBytes(value) : null;
    }

    /// <summary>
    /// The get bytes.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <param name="encoding">
    /// The encoding. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte[] GetBytes(this string value, Encoding encoding)
    {
      if (!((encoding == null) || string.IsNullOrEmpty(value)))
      {
        return encoding.GetBytes(value);
      }

      return null;
    }

    /// <summary>
    /// The get sha 1 hash hex string.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string GetSha1HashHexString(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return string.Empty;
      }

      var cng = new SHA1Cng();
      var buffer = cng.ComputeHash(value.GetBytes(Encoding.GetEncoding(0x4e3)));
      cng.Dispose();
      return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    /// <summary>
    /// The get sh a 256 hash hex string.
    /// </summary>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <returns>
    /// The <see cref="string"/> . 
    /// </returns>
    public static string GetSha256HashHexString(this string value)
    {
      if (string.IsNullOrEmpty(value))
      {
        return string.Empty;
      }

      var cng = new SHA256Cng();
      var buffer = cng.ComputeHash(value.GetBytes(Encoding.GetEncoding(0x4e3)));
      cng.Dispose();
      return BitConverter.ToString(buffer).Replace("-", string.Empty);
    }

    /// <summary>
    /// The is valid hex string.
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public static bool IsValidHexString(this string str)
    {
      if (!Regex.IsMatch(str, "^([a-f|A-F|0-9]{2})+$"))
      {
        str = str.Replace(" ", string.Empty);
      }

      return Regex.IsMatch(str, "^([a-f|A-F|0-9]{2})+$");
    }

    /// <summary>
    /// The nsxe.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="ns">
    /// The ns. 
    /// </param>
    /// <param name="children">
    /// The children. 
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/> . 
    /// </returns>
    public static XElement Nsxe(
      this string instance, 
      [Optional] [DefaultParameterValue(null)] XNamespace ns, 
      params object[] children)
    {
      return new XElement((ns != null) ? ns + instance : instance, children);
    }

    /// <summary>
    /// The to int 16.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <returns>
    /// The <see cref="short"/> . 
    /// </returns>
    public static short ToInt16(this string instance)
    {
      return short.Parse(instance);
    }

    /// <summary>
    /// The to int 32.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public static int ToInt32(this string instance)
    {
      return int.Parse(instance);
    }

    /// <summary>
    /// The to int 64.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <returns>
    /// The <see cref="long"/> . 
    /// </returns>
    public static long ToInt64(this string instance)
    {
      return long.Parse(instance);
    }

    /// <summary>
    /// The to int 8.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <returns>
    /// The <see cref="byte"/> . 
    /// </returns>
    public static byte ToInt8(this string instance)
    {
      return byte.Parse(instance);
    }

    /// <summary>
    /// The xa.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="value">
    /// The value. 
    /// </param>
    /// <returns>
    /// The <see cref="XAttribute"/> . 
    /// </returns>
    public static XAttribute Xa(this string instance, object value)
    {
      return new XAttribute(instance, value);
    }

    /// <summary>
    /// The xe.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="children">
    /// The children. 
    /// </param>
    /// <returns>
    /// The <see cref="XElement"/> . 
    /// </returns>
    public static XElement Xe(this string instance, params object[] children)
    {
      return new XElement(instance, children);
    }

    /// <summary>
    /// The xmlns.
    /// </summary>
    /// <param name="instance">
    /// The instance. 
    /// </param>
    /// <param name="prefix">
    /// The prefix. 
    /// </param>
    /// <returns>
    /// The <see cref="XAttribute"/> . 
    /// </returns>
    public static XAttribute Xmlns(this string instance, [Optional] [DefaultParameterValue(null)] string prefix)
    {
      return string.IsNullOrEmpty(prefix)
               ? new XAttribute("xmlns", instance)
               : new XAttribute(XNamespace.Xmlns + prefix, instance);
    }

    #endregion
  }
}