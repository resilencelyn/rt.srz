// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumbersHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The numbers helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System;
  using System.Globalization;
  using System.Text;

  #endregion

  /// <summary>
  ///   The numbers helper.
  /// </summary>
  public static class NumbersHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get decimal digit value.
    /// </summary>
    /// <param name="ch">
    /// The ch.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// </exception>
    public static byte GetDecimalDigitValue(char ch)
    {
      var decimalDigitValue = CharUnicodeInfo.GetDecimalDigitValue(ch);
      if ((decimalDigitValue < 0) || (decimalDigitValue > 9))
      {
        throw new ArgumentOutOfRangeException("ch");
      }

      return (byte)decimalDigitValue;
    }

    /// <summary>
    /// The get decimal digit value.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte GetDecimalDigitValue(string s, int index)
    {
      return GetDecimalDigitValue(s[index]);
    }

    /// <summary>
    /// The get decimal digit value.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte GetDecimalDigitValue(StringBuilder s, int index)
    {
      return GetDecimalDigitValue(s[index]);
    }

    /// <summary>
    /// The get number digits.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte GetNumberDigits(long number)
    {
      return GetNumberDigits((ulong)number);
    }

    /// <summary>
    /// The get number digits.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static byte GetNumberDigits(ulong number)
    {
      if (number < 10L)
      {
        return 1;
      }

      if (number < 100L)
      {
        return 2;
      }

      if (number < 0x3e8L)
      {
        return 3;
      }

      if (number < 0x2710L)
      {
        return 4;
      }

      if (number < 0x186a0L)
      {
        return 5;
      }

      if (number < 0xf4240L)
      {
        return 6;
      }

      if (number < 0x989680L)
      {
        return 7;
      }

      if (number < 0x5f5e100L)
      {
        return 8;
      }

      if (number < 0x3b9aca00L)
      {
        return 9;
      }

      if (number < 0x2540be400L)
      {
        return 10;
      }

      if (number < 0x174876e800L)
      {
        return 11;
      }

      if (number < 0xe8d4a51000L)
      {
        return 12;
      }

      if (number < 0x9184e72a000L)
      {
        return 13;
      }

      if (number < 0x5af3107a4000L)
      {
        return 14;
      }

      if (number < 0x38d7ea4c68000L)
      {
        return 15;
      }

      if (number < 0x2386f26fc10000L)
      {
        return 0x10;
      }

      if (number < 0x16345785d8a0000L)
      {
        return 0x11;
      }

      if (number < 0xde0b6b3a7640000L)
      {
        return 0x12;
      }

      if (number < 10000000000000000000L)
      {
        return 0x13;
      }

      return 20;
    }

    /// <summary>
    /// The number to string.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="digits">
    /// The digits.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string NumberToString<T>(T number, byte digits = 0) where T : IFormattable
    {
      if (digits <= 0)
      {
        return number.ToString();
      }

      IFormatProvider formatProvider = null;
      return number.ToString("D" + digits, formatProvider);
    }

    /// <summary>
    /// The split number.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] SplitNumber(long number)
    {
      return SplitNumber(number, 0);
    }

    /// <summary>
    /// The split number.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static byte[] SplitNumber(ulong number)
    {
      return SplitNumber(number, 0);
    }

    /// <summary>
    /// The split number.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="minDigits">
    /// The min digits.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] SplitNumber(long number, byte minDigits)
    {
      return SplitNumber((ulong)number, minDigits);
    }

    /// <summary>
    /// The split number.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <param name="minDigits">
    /// The min digits.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static byte[] SplitNumber(ulong number, byte minDigits)
    {
      var buffer = new byte[Math.Max(GetNumberDigits(number), minDigits)];
      var index = buffer.Length - 1;
      while (true)
      {
        buffer[index] = (byte)(number % 10L);
        if (number < 10L)
        {
          while (--index >= 0)
          {
            buffer[index] = 0;
          }

          return buffer;
        }

        number /= 10L;
        index--;
      }
    }

    /// <summary>
    /// The trim sign.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="short"/>.
    /// </returns>
    public static short TrimSign(short number)
    {
      if (number < 0)
      {
        return 0;
      }

      return number;
    }

    /// <summary>
    /// The trim sign.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int TrimSign(int number)
    {
      if (number < 0)
      {
        return 0;
      }

      return number;
    }

    /// <summary>
    /// The trim sign.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long TrimSign(long number)
    {
      if (number < 0L)
      {
        return 0L;
      }

      return number;
    }

    /// <summary>
    /// The trim sign.
    /// </summary>
    /// <param name="number">
    /// The number.
    /// </param>
    /// <returns>
    /// The <see cref="sbyte"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static sbyte TrimSign(sbyte number)
    {
      if (number < 0)
      {
        return 0;
      }

      return number;
    }

    #endregion
  }
}