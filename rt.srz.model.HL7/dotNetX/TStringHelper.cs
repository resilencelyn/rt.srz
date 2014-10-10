// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TStringHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The t string helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.dotNetX
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Runtime.InteropServices;
  using System.Text;

  #endregion

  /// <summary>
  ///   The t string helper.
  /// </summary>
  public static class TStringHelper
  {
    #region Enums

    /// <summary>
    ///   The t empty string types.
    /// </summary>
    public enum TEmptyStringTypes
    {
      /// <summary>
      ///   The empty.
      /// </summary>
      Empty, 

      /// <summary>
      ///   The null.
      /// </summary>
      Null, 

      /// <summary>
      ///   The signature.
      /// </summary>
      Signature
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The combine multiple strings.
    /// </summary>
    /// <param name="Strings">
    /// The strings.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CombineMultipleStrings(params string[] Strings)
    {
      if (Strings != null)
      {
        var longLength = Strings.LongLength;
        if (longLength > 1L)
        {
          StringBuilder builder = null;
          string str = null;
          var str2 = Strings[(int)((IntPtr)(longLength -= 1L))];
          var flag = !string.IsNullOrEmpty(str2);
          for (var i = 0L; i < longLength; i += 1L)
          {
            var str3 = Strings[(int)((IntPtr)i)];
            if (string.IsNullOrEmpty(str3))
            {
              if (str == null)
              {
                str = str3;
              }
            }
            else if (string.IsNullOrEmpty(str))
            {
              str = str3;
            }
            else
            {
              if (builder == null)
              {
                builder = new StringBuilder(str);
              }

              if (flag)
              {
                builder.Append(str2);
              }

              builder.Append(str3);
            }
          }

          if (builder != null)
          {
            return builder.ToString();
          }

          return str;
        }
      }

      return null;
    }

    /// <summary>
    /// The combine multiple strings.
    /// </summary>
    /// <param name="Strings">
    /// The strings.
    /// </param>
    /// <param name="Delimiter">
    /// The delimiter.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CombineMultipleStrings(IEnumerable<string> Strings, string Delimiter)
    {
      if (Strings == null)
      {
        return null;
      }

      StringBuilder builder = null;
      string str = null;
      var flag = !string.IsNullOrEmpty(Delimiter);
      foreach (var str2 in Strings)
      {
        if (string.IsNullOrEmpty(str2))
        {
          if (str == null)
          {
            str = str2;
          }
        }
        else
        {
          if (string.IsNullOrEmpty(str))
          {
            str = str2;
            continue;
          }

          if (builder == null)
          {
            builder = new StringBuilder(str);
          }

          if (flag)
          {
            builder.Append(Delimiter);
          }

          builder.Append(str2);
        }
      }

      if (builder != null)
      {
        return builder.ToString();
      }

      return str;
    }

    /// <summary>
    /// The combine strings.
    /// </summary>
    /// <param name="LeftString">
    /// The left string.
    /// </param>
    /// <param name="RightString">
    /// The right string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CombineStrings(string LeftString, string RightString)
    {
      return CombineStrings(LeftString, RightString, " ");
    }

    /// <summary>
    /// The combine strings.
    /// </summary>
    /// <param name="Builder">
    /// The builder.
    /// </param>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder CombineStrings(StringBuilder Builder, string String)
    {
      return CombineStrings(Builder, String, " ");
    }

    /// <summary>
    /// The combine strings.
    /// </summary>
    /// <param name="LeftString">
    /// The left string.
    /// </param>
    /// <param name="RightString">
    /// The right string.
    /// </param>
    /// <param name="Delimiter">
    /// The delimiter.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CombineStrings(string LeftString, string RightString, string Delimiter)
    {
      return CombineMultipleStrings(new[] { LeftString, RightString, Delimiter });
    }

    /// <summary>
    /// The combine strings.
    /// </summary>
    /// <param name="Builder">
    /// The builder.
    /// </param>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="Delimiter">
    /// The delimiter.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder CombineStrings(StringBuilder Builder, string String, string Delimiter)
    {
      if (!string.IsNullOrEmpty(String))
      {
        if (Builder == null)
        {
          return new StringBuilder(String);
        }

        if (Builder.Length > 0)
        {
          Builder.Append(Delimiter);
        }

        Builder.Append(String);
      }

      return Builder;
    }

    /// <summary>
    /// The compact string.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CompactString(string str)
    {
      return CompactString(str, ' ');
    }

    /// <summary>
    /// The compact string.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="Space">
    /// The space.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string CompactString(string str, char Space)
    {
      if (IsNullOrEmpty(str))
      {
        return str;
      }

      StringBuilder builder = null;
      var startIndex = 0;
      var num2 = -1;
      var length = str.Length;
      for (var i = 0; i < length; i++)
      {
        if (char.IsWhiteSpace(str, i))
        {
          if (num2 < 0)
          {
            num2 = i;
          }
        }
        else if (num2 >= 0)
        {
          if (((i - num2) == 1) && (str[num2] == Space))
          {
            num2 = -1;
          }
          else
          {
            if (builder == null)
            {
              builder = new StringBuilder();
            }

            builder.Append(str.Substring(startIndex, num2 - startIndex));
            if (Space != '\0')
            {
              builder.Append(Space);
            }

            startIndex = i;
            num2 = -1;
          }
        }
      }

      if (builder == null)
      {
        return str;
      }

      if (startIndex < length)
      {
        builder.Append(str.Substring(startIndex));
      }

      return builder.ToString();
    }

    /// <summary>
    /// The convert to xml string.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ConvertToXmlString(string String)
    {
      return ConvertToXmlString(String, false);
    }

    /// <summary>
    /// The convert to xml string.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <param name="bDoubleApostrophe">
    /// The b double apostrophe.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ConvertToXmlString(string str, bool bDoubleApostrophe)
    {
      return
        str.Replace("&", "&amp;")
           .Replace("'", bDoubleApostrophe ? "&apos;&apos;" : "&apos;")
           .Replace("<", "&lt;")
           .Replace(">", "&gt;")
           .Replace("\"", "&quot;");
    }

    /// <summary>
    /// The is null or empty.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrEmpty(string str)
    {
      if (str != null)
      {
        return str.Length < 1;
      }

      return true;
    }

    /// <summary>
    /// The is null or empty.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrEmpty(StringBuilder str)
    {
      if (str != null)
      {
        return str.Length < 1;
      }

      return true;
    }

    /// <summary>
    /// The is null or white space.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrWhiteSpace(string str)
    {
      if (str != null)
      {
        var length = str.Length;
        while (length > 0)
        {
          if (!char.IsWhiteSpace(str[--length]))
          {
            return false;
          }
        }
      }

      return true;
    }

    /// <summary>
    /// The is null or white space.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrWhiteSpace(StringBuilder str)
    {
      if (str != null)
      {
        var length = str.Length;
        while (length > 0)
        {
          if (!char.IsWhiteSpace(str[--length]))
          {
            return false;
          }
        }
      }

      return true;
    }

    /// <summary>
    /// The replace any.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="replaceTo">
    /// The replace to.
    /// </param>
    /// <param name="searchFor">
    /// The search for.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceAny(string s, char replaceTo, params char[] searchFor)
    {
      if (string.IsNullOrEmpty(s))
      {
        return s;
      }

      return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    }

    /// <summary>
    /// The replace any.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="replaceTo">
    /// The replace to.
    /// </param>
    /// <param name="searchFor">
    /// The search for.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder ReplaceAny(StringBuilder s, char replaceTo, params char[] searchFor)
    {
      if (IsNullOrEmpty(s))
      {
        return s;
      }

      return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    }

    /// <summary>
    /// The replace any.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="replaceTo">
    /// The replace to.
    /// </param>
    /// <param name="searchFor">
    /// The search for.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ReplaceAny(string s, int index, int count, char replaceTo, params char[] searchFor)
    {
      if ((s != null) && (searchFor != null))
      {
        var length = searchFor.Length;
        if (length <= 0)
        {
          return s;
        }

        if (index < 0)
        {
          index = 0;
        }

        var num2 = index + count;
        if (num2 > s.Length)
        {
          num2 = s.Length;
        }

        while (index < num2)
        {
          var ch = s[index];
          var num3 = length;
          while (num3 > 0)
          {
            if (ch == searchFor[--num3])
            {
              return ReplaceAny(new StringBuilder(s), index, num2 - index, replaceTo, searchFor).ToString();
            }
          }

          index++;
        }
      }

      return s;
    }

    /// <summary>
    /// The replace any.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="index">
    /// The index.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <param name="replaceTo">
    /// The replace to.
    /// </param>
    /// <param name="searchFor">
    /// The search for.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder ReplaceAny(
      StringBuilder s, 
      int index, 
      int count, 
      char replaceTo, 
      params char[] searchFor)
    {
      if ((s != null) && (searchFor != null))
      {
        var length = searchFor.Length;
        if (length <= 0)
        {
          return s;
        }

        if (index < 0)
        {
          index = 0;
        }

        var num2 = index + count;
        if (num2 > s.Length)
        {
          num2 = s.Length;
        }

        while (index < num2)
        {
          var ch = s[index];
          var num3 = length;
          while (num3 > 0)
          {
            if (ch == searchFor[--num3])
            {
              s[index] = replaceTo;
              break;
            }
          }

          index++;
        }
      }

      return s;
    }

    /// <summary>
    /// The shrink to length.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="MaxLength">
    /// The max length.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ShrinkToLength(string str, int MaxLength)
    {
      if (!IsNullOrEmpty(str) && (str.Length > MaxLength))
      {
        return str.Substring(0, (MaxLength > 0) ? MaxLength : 0);
      }

      return str;
    }

    /// <summary>
    /// The shrink to length.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="MaxLength">
    /// The max length.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder ShrinkToLength(StringBuilder str, int MaxLength)
    {
      if (!IsNullOrEmpty(str) && (str.Length > MaxLength))
      {
        str.Length = (MaxLength > 0) ? MaxLength : 0;
      }

      return str;
    }

    /// <summary>
    /// The string builder to string.
    /// </summary>
    /// <param name="sb">
    /// The sb.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringBuilderToString(StringBuilder sb)
    {
      if (sb == null)
      {
        return null;
      }

      return sb.ToString();
    }

    /// <summary>
    /// The string from bytes.
    /// </summary>
    /// <param name="Bytes">
    /// The bytes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringFromBytes(byte[] Bytes)
    {
      return StringFromBytes(Bytes, 0L);
    }

    /// <summary>
    /// The string from bytes.
    /// </summary>
    /// <param name="Bytes">
    /// The bytes.
    /// </param>
    /// <param name="Offset">
    /// The offset.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringFromBytes(byte[] Bytes, long Offset)
    {
      return StringFromBytes(Bytes, Offset, 0x7fffffffffffffffL);
    }

    /// <summary>
    /// The string from bytes.
    /// </summary>
    /// <param name="Bytes">
    /// The bytes.
    /// </param>
    /// <param name="Offset">
    /// The offset.
    /// </param>
    /// <param name="Count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringFromBytes(byte[] Bytes, long Offset, long Count)
    {
      if (((Bytes != null) && (Bytes.LongLength > 0L)) && (Offset >= 0L))
      {
        var num = Bytes.LongLength - Offset;
        if (num > 0L)
        {
          if (Count > num)
          {
            Count = num;
          }

          if (Count > 0L)
          {
            Encoding unicode = null;
            if (unicode == null)
            {
              var preamble = Encoding.Unicode.GetPreamble();
              if (((preamble != null) && (preamble.Length > 0)) && (preamble.Length <= Count))
              {
                long length = preamble.Length;
                do
                {
                  length -= 1L;
                  if (Bytes[(int)((IntPtr)(Offset + length))] != preamble[(int)((IntPtr)length)])
                  {
                    length = -9223372036854775808L;
                    break;
                  }
                }
                while (length > 0L);
                if (length != -9223372036854775808L)
                {
                  Count -= preamble.Length;
                  if (Count <= 0L)
                  {
                    return null;
                  }

                  unicode = Encoding.Unicode;
                  Offset += preamble.Length;
                }
              }
            }

            if (unicode == null)
            {
              var buffer2 = Encoding.BigEndianUnicode.GetPreamble();
              if (((buffer2 != null) && (buffer2.Length > 0)) && (buffer2.Length <= Count))
              {
                long num3 = buffer2.Length;
                do
                {
                  num3 -= 1L;
                  if (Bytes[(int)((IntPtr)(Offset + num3))] != buffer2[(int)((IntPtr)num3)])
                  {
                    num3 = -9223372036854775808L;
                    break;
                  }
                }
                while (num3 > 0L);
                if (num3 != -9223372036854775808L)
                {
                  Count -= buffer2.Length;
                  if (Count <= 0L)
                  {
                    return null;
                  }

                  unicode = Encoding.BigEndianUnicode;
                  Offset += buffer2.Length;
                }
              }
            }

            if (unicode == null)
            {
              unicode = Encoding.Unicode;
            }

            return unicode.GetString(Bytes, (int)Offset, (int)Count);
          }
        }
      }

      return null;
    }

    /// <summary>
    /// The string shrink.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="EmptyType">
    /// The empty type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringShrink(string str, TEmptyStringTypes EmptyType)
    {
      return StringShrink(str, EmptyType, false);
    }

    /// <summary>
    /// The string shrink.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="EmptyType">
    /// The empty type.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringShrink(StringBuilder String, TEmptyStringTypes EmptyType)
    {
      return StringShrink(String, EmptyType, false);
    }

    /// <summary>
    /// The string shrink.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="EmptyType">
    /// The empty type.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringShrink(string str, TEmptyStringTypes EmptyType, bool bTrimFirst)
    {
      if (bTrimFirst && (str != null))
      {
        str = str.Trim();
      }

      if (string.IsNullOrEmpty(str))
      {
        switch (EmptyType)
        {
          case TEmptyStringTypes.Empty:
            return string.Empty;

          case TEmptyStringTypes.Null:
            return null;

          case TEmptyStringTypes.Signature:
            return "null";
        }
      }

      return str;
    }

    /// <summary>
    /// The string shrink.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="EmptyType">
    /// The empty type.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringShrink(StringBuilder String, TEmptyStringTypes EmptyType, bool bTrimFirst)
    {
      return StringShrink(StringBuilderToString(String), EmptyType, bTrimFirst);
    }

    /// <summary>
    /// The string to bytes.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] StringToBytes(string String)
    {
      return StringToBytes(String, false);
    }

    /// <summary>
    /// The string to bytes.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bUseBigEndian">
    /// The b use big endian.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] StringToBytes(string String, bool bUseBigEndian)
    {
      return StringToBytes(String, bUseBigEndian, false);
    }

    /// <summary>
    /// The string to bytes.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bUseBigEndian">
    /// The b use big endian.
    /// </param>
    /// <param name="bWriteBOM">
    /// The b write bom.
    /// </param>
    /// <returns>
    /// The <see cref="byte[]"/>.
    /// </returns>
    public static byte[] StringToBytes(string String, bool bUseBigEndian, bool bWriteBOM)
    {
      if (string.IsNullOrEmpty(String))
      {
        return null;
      }

      var encoding = bUseBigEndian ? Encoding.BigEndianUnicode : Encoding.Unicode;
      var bytes = encoding.GetBytes(String);
      if (bWriteBOM)
      {
        var preamble = encoding.GetPreamble();
        if ((preamble != null) && (preamble.LongLength > 0L))
        {
          var destinationArray = new byte[preamble.LongLength + bytes.LongLength];
          Array.Copy(preamble, destinationArray, preamble.LongLength);
          Array.Copy(bytes, 0L, destinationArray, preamble.LongLength, bytes.LongLength);
          bytes = destinationArray;
        }
      }

      return bytes;
    }

    /// <summary>
    /// The string to empty.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToEmpty(string String)
    {
      return StringToEmpty(String, false);
    }

    /// <summary>
    /// The string to empty.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToEmpty(StringBuilder String)
    {
      return StringToEmpty(String, false);
    }

    /// <summary>
    /// The string to empty.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToEmpty(string String, bool bTrimFirst)
    {
      return StringShrink(String, TEmptyStringTypes.Empty, bTrimFirst);
    }

    /// <summary>
    /// The string to empty.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToEmpty(StringBuilder String, bool bTrimFirst)
    {
      return StringShrink(String, TEmptyStringTypes.Empty, bTrimFirst);
    }

    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(string String)
    {
      return StringToNull(String, false);
    }

    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(StringBuilder String)
    {
      return StringToNull(String, false);
    }

    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(string String, bool bTrimFirst)
    {
      return StringShrink(String, TEmptyStringTypes.Null, bTrimFirst);
    }

    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="String">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(StringBuilder String, bool bTrimFirst)
    {
      return StringShrink(String, TEmptyStringTypes.Null, bTrimFirst);
    }

    /// <summary>
    /// The string to string builder.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder StringToStringBuilder(string s)
    {
      if (s == null)
      {
        return null;
      }

      return new StringBuilder(s);
    }

    #endregion

    /// <summary>
    ///   The readonly string.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReadonlyString
    {
      /// <summary>
      ///   The _string.
      /// </summary>
      private readonly string _string;

      /// <summary>
      ///   The _string builder.
      /// </summary>
      private readonly StringBuilder _stringBuilder;

      /// <summary>
      /// Initializes a new instance of the <see cref="ReadonlyString"/> struct.
      /// </summary>
      /// <param name="str">
      /// The str.
      /// </param>
      public ReadonlyString(string str)
      {
        _string = str;
        _stringBuilder = null;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ReadonlyString"/> struct.
      /// </summary>
      /// <param name="str">
      /// The str.
      /// </param>
      public ReadonlyString(StringBuilder str)
      {
        _string = null;
        _stringBuilder = str;
      }

      /// <summary>
      ///   The op_ implicit.
      /// </summary>
      /// <param name="str">
      ///   The str.
      /// </param>
      /// <returns>
      /// </returns>
      public static implicit operator ReadonlyString(string str)
      {
        return new ReadonlyString(str);
      }

      /// <summary>
      ///   The op_ implicit.
      /// </summary>
      /// <param name="str">
      ///   The str.
      /// </param>
      /// <returns>
      /// </returns>
      public static implicit operator ReadonlyString(StringBuilder str)
      {
        return new ReadonlyString(str);
      }

      /// <summary>
      ///   The is null or empty.
      /// </summary>
      /// <returns>
      ///   The <see cref="bool" />.
      /// </returns>
      public bool IsNullOrEmpty()
      {
        if (_string == null)
        {
          return TStringHelper.IsNullOrEmpty(_stringBuilder);
        }

        return TStringHelper.IsNullOrEmpty(_string);
      }

      /// <summary>
      ///   The is null or white space.
      /// </summary>
      /// <returns>
      ///   The <see cref="bool" />.
      /// </returns>
      public bool IsNullOrWhiteSpace()
      {
        if (_string == null)
        {
          return TStringHelper.IsNullOrWhiteSpace(_stringBuilder);
        }

        return TStringHelper.IsNullOrWhiteSpace(_string);
      }

      /// <summary>
      ///   The to string.
      /// </summary>
      /// <returns>
      ///   The <see cref="string" />.
      /// </returns>
      public override string ToString()
      {
        if (_string == null)
        {
          return _stringBuilder.ToString();
        }

        return _string;
      }

      /// <summary>
      /// The this.
      /// </summary>
      /// <param name="index">
      /// The index.
      /// </param>
      /// <returns>
      /// The <see cref="char"/>.
      /// </returns>
      public char this[int index]
      {
        get
        {
          if (_string == null)
          {
            return _stringBuilder[index];
          }

          return _string[index];
        }
      }

      /// <summary>
      ///   Gets the length.
      /// </summary>
      public int Length
      {
        get
        {
          if (_string == null)
          {
            return _stringBuilder.Length;
          }

          return _string.Length;
        }
      }
    }
  }
}