// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TStringHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The t string helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

namespace rt.srz.database.business.standard.helpers
{
  using System.Text;

  using rt.srz.database.business.standard.enums;

  /// <summary>
  /// The t string helper.
  /// </summary>
  public static class TStringHelper
  {
    #region ReadonlyString

    /*
        // ������ ������ ��� ������
        public struct ReadonlyString
        {
            // ����������� ��� ������� str
            //public ReadonlyString(str str)
            //{
            //    _string = str;
            //    _stringBuilder = null;
            //}

            // ����������� ��� ������� str
            //public ReadonlyString(StringBuilder str)
            //{
            //    _string = null;
            //    _stringBuilder = str;
            //}

            // �������������� �� ������� str
            //public static implicit operator ReadonlyString(str str)
            //{
            //    return new ReadonlyString(str);
            //}

            // �������������� �� ������� str
            //public static implicit operator ReadonlyString(StringBuilder str)
            //{
            //    return new ReadonlyString(str);
            //}

            //---------------------------------------

            // ��������: ������ ��� ������� ������
            //public bool IsNullOrEmpty()
            //{
            //    return (_string != null) ? TStringHelper.IsNullOrEmpty(_string) : TStringHelper.IsNullOrEmpty(_stringBuilder);
            //}

            // ��������: ������� ������ ��� ���������� ������ ���������� �������
            //public bool IsNullOrWhiteSpace()
            //{
            //    return (_string != null) ? TStringHelper.IsNullOrWhiteSpace(_string) : TStringHelper.IsNullOrWhiteSpace(_stringBuilder);
            //}

            // ������������� � ������
            //public override string ToString()
            //{
            //    return (_string != null) ? _string : _stringBuilder.ToString();
            //}

            // �������� ������ � �������� �������
            //public char this[int index]
            //{
            //    get
            //    {
            //        return (_string != null) ? _string[index] : _stringBuilder[index];
            //    }
            //}

            // ����� ������
            //public int Length
            //{
            //    get
            //    {
            //        return (_string != null) ? _string.Length : _stringBuilder.Length;
            //    }
            //}

            //---------------------------------------

            //str _string;
            //StringBuilder _stringBuilder;
        }
        */
    #endregion

    #region IsNullOrEmpty

    // IsNullOrEmpty: ������������� ���������� (��� str, StringBuilder...)
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
      return str == null || str.Length < 1;
    }

    // IsNullOrEmpty: ������������� ���������� (��� str, StringBuilder...)
    // public static bool IsNullOrEmpty(StringBuilder str)
    // {
    // return (str == null || str.Length < 1);
    // }

    // ���������, �������� �� ������ �������, ������ ��� �������� ������ ���������� �������
    // public static bool IsNullOrWhiteSpace(str str)
    // {
    // if (str == null)
    // return true;
    // int i = str.Length;
    // while (i > 0)
    // {
    // if (!char.IsWhiteSpace(str[--i]))
    // return false;
    // }
    // return true;
    // }

    // ���������� IsNullOrWhiteSpace()
    // !! ��� StringBuilder
    // public static bool IsNullOrWhiteSpace(StringBuilder str)
    // {
    // if (str == null)
    // return true;
    // int i = str.Length;
    // while (i > 0)
    // {
    // if (!char.IsWhiteSpace(str[--i]))
    // return false;
    // }
    // return true;
    // }
    #endregion

    #region string of bytes

    // ������������� ������ � ������ ����
    // !! ���� ������ �����, �����. null
    // public static byte[] StringToBytes(string str, bool bUseBigEndian, bool bWriteBOM)
    // {
    // // �� ������ ������ ���������� null
    // if (string.IsNullOrEmpty(str))
    // return null;
    // // �������� ����������
    // Encoding Encoding = (bUseBigEndian) ? Encoding.BigEndianUnicode : Encoding.Unicode;
    // // ��������
    // byte[] Result = Encoding.GetBytes(str);
    // // ���� �����, �������� BOM-���������
    // if (bWriteBOM)
    // {
    // #if SILVERLIGHT
    // byte[] BOM = Encoding.GetPreamble();
    // if (BOM != null && BOM.Length > 0)
    // {
    // byte[] UnitedArray = new byte[BOM.Length + Result.Length];
    // Array.Copy(BOM, UnitedArray, BOM.Length);
    // Array.Copy(Result, 0, UnitedArray, BOM.Length, Result.Length);
    // Result = UnitedArray;
    // }
    // #else
    // byte[] BOM = Encoding.GetPreamble();
    // if (BOM != null && BOM.LongLength > 0)
    // {
    // byte[] UnitedArray = new byte[BOM.LongLength + Result.LongLength];
    // Array.Copy(BOM, UnitedArray, BOM.LongLength);
    // Array.Copy(Result, 0, UnitedArray, BOM.LongLength, Result.LongLength);
    // Result = UnitedArray;
    // }
    // #endif
    // }
    // // ������
    // return Result;
    // }

    // ���������� ����� StringToBytes()
    // !! bWriteBOM == false
    // public static byte[] StringToBytes(string str, bool bUseBigEndian)
    // {
    // return StringToBytes(str, bUseBigEndian, /*bWriteBOM*/ false);
    // }

    // ���������� ����� StringToBytes()
    // !! bWriteBOM == false
    // !! bUseBigEndian == false
    // public static byte[] StringToBytes(string str)
    // {
    // return StringToBytes(str, /*bUseBigEndian*/ false);
    // }

    // ������������� ������ ���� � ������
    // !! ���� ������ ���� ����, �����. null
    // public static string StringFromBytes(byte[] Bytes, long Offset, long Count)
    // {
    // if
    // (
    // Bytes != null
    // &&
    // #if SILVERLIGHT
    // Bytes.Length > 0
    // #else
    // Bytes.LongLength > 0
    // #endif
    // &&
    // Offset >= 0
    // )
    // {
    // #if SILVERLIGHT
    // long MaxCount = Bytes.Length - Offset;
    // #else
    // long MaxCount = Bytes.LongLength - Offset;
    // #endif
    // if (MaxCount > 0)
    // {
    // // ��������� Count
    // if (Count > MaxCount)
    // Count = MaxCount;
    // // ����������, ��� ���� ����� � �������������
    // if (Count > 0)
    // {
    // // ���������� ������������
    // Encoding Encoding = null;
    // // ����������� BOM-��������� ��� Little-Endian �������
    // if (Encoding == null)
    // {
    // byte[] BOM = Encoding.Unicode.GetPreamble();
    // if (BOM != null && BOM.Length > 0 && BOM.Length <= Count)
    // {
    // long idx = BOM.Length;
    // do
    // {
    // --idx;
    // if (Bytes[Offset + idx] != BOM[idx])
    // {
    // idx = long.MinValue;
    // break;
    // }
    // }
    // while (idx > 0);
    // // ��������� ����������
    // if (idx != long.MinValue)
    // {
    // Count -= BOM.Length;
    // if (Count <= 0)
    // return null;
    // Encoding = Encoding.Unicode;
    // Offset += BOM.Length;
    // }
    // }
    // }
    // // ����������� BOM-��������� ��� Big-Endian �������
    // if (Encoding == null)
    // {
    // byte[] BOM = Encoding.BigEndianUnicode.GetPreamble();
    // if (BOM != null && BOM.Length > 0 && BOM.Length <= Count)
    // {
    // long idx = BOM.Length;
    // do
    // {
    // --idx;
    // if (Bytes[Offset + idx] != BOM[idx])
    // {
    // idx = long.MinValue;
    // break;
    // }
    // }
    // while (idx > 0);
    // // ��������� ����������
    // if (idx != long.MinValue)
    // {
    // Count -= BOM.Length;
    // if (Count <= 0)
    // return null;
    // Encoding = Encoding.BigEndianUnicode;
    // Offset += BOM.Length;
    // }
    // }
    // }
    // // �� ��������� ���������� Unicode-Encoding
    // if (Encoding == null)
    // Encoding = Encoding.Unicode;
    // // ����������
    // return Encoding.GetString(Bytes, (int)Offset, (int)Count);
    // }
    // }
    // }
    // // ������
    // return null;
    // }

    // ���������� ����� StringFromBytes()
    // !! Count ������� �� ����� �������
    // public static string StringFromBytes(byte[] Bytes, long Offset)
    // {
    // return StringFromBytes(Bytes, Offset, /*Count*/ long.MaxValue);
    // }

    // ���������� ����� StringFromBytes()
    // !! Count ������� �� ����� �������
    // !! Offset == 0
    // public static string StringFromBytes(byte[] Bytes)
    // {
    // return StringFromBytes(Bytes, /*Offset*/ 0);
    // }
    #endregion

    #region strings concatenation

    // ������� ��������� �����, ��������� ����������� ����� ����
    // !! ��������� �������� ������ ��������� ������������
    // public static string CombineMultipleStrings(params string[] Strings)
    // {
    // if (Strings != null)
    // {
    // #if SILVERLIGHT
    // long StringsCount = Strings.Length;
    // #else
    // long StringsCount = Strings.LongLength;
    // #endif
    // if (StringsCount > 1)
    // {
    // StringBuilder ResultString = null;
    // string FirstNonNullString = null;
    // // !! ��������� ������ - ��� �����������
    // string Delimiter = Strings[--StringsCount];
    // bool bHasDelimiter = !string.IsNullOrEmpty(Delimiter);
    // for (long i = 0; i < StringsCount; ++i)
    // {
    // string CurrentString = Strings[i];
    // if (string.IsNullOrEmpty(CurrentString))
    // {
    // // ���������� ������ ��������� ������ !! ��� ����� ���� ������ ������
    // if (FirstNonNullString == null)
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // ���� �� ������� ��������� �������� ������, ������ ���������� ��
    // if (string.IsNullOrEmpty(FirstNonNullString))
    // {
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // ���� �������� ������ ��� ����, ����� ������� ���������
    // if (ResultString == null)
    // ResultString = new StringBuilder(FirstNonNullString);
    // if (bHasDelimiter)
    // ResultString.Append(Delimiter);
    // ResultString.Append(CurrentString);
    // }
    // }
    // }
    // // ���� ���� ��������� ������������ �����, ����� ���������� ��
    // if (ResultString != null)
    // return ResultString.ToString();
    // // � ��������� ������ ���������� ������ �� ��������� ������
    // return FirstNonNullString;
    // }
    // }
    // // ��� ����� �� �����
    // return null;
    // }

    // ������� ��������� �����, ��������� ����������� ����� ����
    // public static string CombineMultipleStrings(IEnumerable<string> Strings, string Delimiter)
    // {
    // if (Strings != null)
    // {
    // StringBuilder ResultString = null;
    // string FirstNonNullString = null;
    // bool bHasDelimiter = !string.IsNullOrEmpty(Delimiter);
    // foreach (var CurrentString in Strings)
    // {
    // if (string.IsNullOrEmpty(CurrentString))
    // {
    // // ���������� ������ ��������� ������ !! ��� ����� ���� ������ ������
    // if (FirstNonNullString == null)
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // ���� �� ������� ��������� �������� ������, ������ ���������� ��
    // if (string.IsNullOrEmpty(FirstNonNullString))
    // {
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // ���� �������� ������ ��� ����, ����� ������� ���������
    // if (ResultString == null)
    // ResultString = new StringBuilder(FirstNonNullString);
    // if (bHasDelimiter)
    // ResultString.Append(Delimiter);
    // ResultString.Append(CurrentString);
    // }
    // }
    // }
    // // ���� ���� ��������� ������������ �����, ����� ���������� ��
    // if (ResultString != null)
    // return ResultString.ToString();
    // // � ��������� ������ ���������� ������ �� ��������� ������
    // return FirstNonNullString;
    // }
    // // ��� ����� �� �����
    // return null;
    // }

    // ������� ��� ������
    // !! ���� ��� ������ �� �����, ����� ���� �������� �������� �����������
    // public static string CombineStrings(string LeftString, string RightString, string Delimiter)
    // {
    // return CombineMultipleStrings(LeftString, RightString, Delimiter);
    // }

    // ���������� ����� CombineStrings()
    // !! Delimiter == " "
    // public static string CombineStrings(string LeftString, string RightString)
    // {
    // return CombineStrings(LeftString, RightString, /*Delimiter*/ " ");
    // }

    // �������� ������ � ���������� ������
    // !! ���� Builder == null, �� ��������� ��� ������ ������
    // !! ���� � ������� ���� �����, ����� ����� ������� �������� �������� �����������; ���� ������ ���, �� ������ ������ ����������� � ������
    // !! ���� str ������ ��� �������, ������ �� ����������
    // <returns>���������� ������, ���������� �� �����</returns>
    // public static StringBuilder CombineStrings(StringBuilder Builder, string str, string Delimiter)
    // {
    // if (!string.IsNullOrEmpty(str))
    // {
    // if (Builder == null)
    // return new StringBuilder(str);
    // if (Builder.Length > 0)
    // Builder.Append(Delimiter);
    // Builder.Append(str);
    // }
    // // ������
    // return Builder;
    // }

    // ���������� ����� CombineStrings()
    // !! Delimiter == " "
    // public static StringBuilder CombineStrings(StringBuilder Builder, string str)
    // {
    // return CombineStrings(Builder, str, /*Delimiter*/ " ");
    // }
    #endregion

    #region str <-> StringBuilder

    // ������������� str � StringBuilder
    // !! ���� �� ����� null, ���������� ����� null
    // public static StringBuilder StringToStringBuilder(str s)
    // {
    // if (s == null)
    // return null;
    // return new StringBuilder(s);
    // }

    // ������������� StringBuilder � str
    // !! ���� �� ����� null, ���������� ����� null
    // public static string StringBuilderToString(StringBuilder sb)
    // {
    // if (sb == null)
    // return null;
    // return sb.ToString();
    // }
    #endregion

    #region empty strings

    // �������� ������ � ��������� ����, ���� ��� ������
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
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
      if (bTrimFirst && str != null)
        str = str.Trim();
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

    // ���������� ������ StringShrink()
    // !! bTrimFirst == false
    // public static string StringShrink(string str, TEmptyStringTypes EmptyType)
    // {
    // return StringShrink(str, EmptyType, /*bTrimFirst*/ false);
    // }

    // �������� ������ � null, ���� ��� ������
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
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

    // ���������� ������ StringToNull()
    // !! bTrimFirst == false
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
      return StringToNull(String, /*bTrimFirst*/ false);
    }

    // ���������� string.Empty, ���� �� ����� ������ ������ (� �.�. null)
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
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

    // ���������� ������ StringToEmpty()
    // !! bTrimFirst == false
    // public static string StringToEmpty(string str)
    // {
    // return StringToEmpty(str, /*bTrimFirst*/ false);
    // }

    // �������������� ������ � ������� XML
    // public static string ConvertToXmlString(string str, bool bDoubleApostrophe)
    // {
    // return str.Replace("&", "&amp;").Replace("'", (bDoubleApostrophe) ? "&apos;&apos;" : "&apos;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    // }

    // ���������� ������ ConvertToXmlString()
    // !! bDoubleApostrophe == false
    // public static string ConvertToXmlString(string str)
    // {
    // return ConvertToXmlString(str, /*bDoubleApostrophe*/ false);
    // }
    #endregion

    #region empty strings ��� StringBuilder

    // �������� ������ � ��������� ����, ���� ��� ������
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
    // public static string StringShrink(StringBuilder str, TEmptyStringTypes EmptyType, bool bTrimFirst)
    // {
    // return StringShrink(StringBuilderToString(str), EmptyType, bTrimFirst);
    // }

    // ���������� ������ StringShrink()
    // !! bTrimFirst == false
    // public static string StringShrink(StringBuilder str, TEmptyStringTypes EmptyType)
    // {
    // return StringShrink(str, EmptyType, /*bTrimFirst*/ false);
    // }

    // �������� ������ � null, ���� ��� ������
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
    // public static string StringToNull(StringBuilder str, bool bTrimFirst)
    // {
    // return StringShrink(str, TEmptyStringTypes.Null, bTrimFirst);
    // }

    // ���������� ������ StringToNull()
    // !! bTrimFirst == false
    // public static string StringToNull(StringBuilder str)
    // {
    // return StringToNull(str, /*bTrimFirst*/ false);
    // }

    // ���������� string.Empty, ���� �� ����� ������ ������ (� �.�. null)
    // !! �������� ������ ������������ ���-����, � ���������� ������� bTrimFirst
    // public static string StringToEmpty(StringBuilder str, bool bTrimFirst)
    // {
    // return StringShrink(str, TEmptyStringTypes.Empty, bTrimFirst);
    // }

    // ���������� ������ StringToEmpty()
    // !! bTrimFirst == false
    // public static string StringToEmpty(StringBuilder str)
    // {
    // return StringToEmpty(str, /*bTrimFirst*/ false);
    // }
    #endregion

    #region string shrink

    // �������� ������ ������ � ��� ������, ���� �� ����� ��������� ��������
    // public static string ShrinkToLength(string str, int MaxLength)
    // {
    // if (!IsNullOrEmpty(str))
    // {
    // if (str.Length > MaxLength)
    // return str.Substring(0, (MaxLength > 0) ? MaxLength : 0);
    // }
    // return str;
    // }

    // ������ ShrinkToLength() ��� StringBuilder
    // public static StringBuilder ShrinkToLength(StringBuilder str, int MaxLength)
    // {
    // if (!IsNullOrEmpty(str))
    // {
    // if (str.Length > MaxLength)
    // str.Length = (MaxLength > 0) ? MaxLength : 0;
    // }
    // return str;
    // }

    // ��������� ������, ������� ��� ������������� �������/���������/� �.�. �� �������� ������
    // !! ����� ������ Space = '\0', ����� ��������� "� ����"
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
      if (!IsNullOrEmpty(str))
      {
        StringBuilder result = null;
        int start = 0, space = -1, len = str.Length;
        for (var i = 0; i < len; ++i)
        {
          if (char.IsWhiteSpace(str, i))
          {
            if (space < 0)
              space = i;
          }
          else
          {
            if (space >= 0)
            {
              // �����������: ���� ������ ������ ���� � �� ����� ��������� Space-�������, ������ ������ �� ����
              if (i - space == 1 && str[space] == Space)
              {
                space = -1;
                continue;
              }

              if (result == null)
                result = new StringBuilder();
              result.Append(str.Substring(start, space - start));
              if (Space != '\0')
                result.Append(Space);
              start = i;
              space = -1;
            }
          }
        }

// ���� ���-�� ��������, ���������� ���������� ������
        if (result != null)
        {
          if (start < len)
            result.Append(str.Substring(start));
          return result.ToString();
        }
      }

      return str;
    }

    // ���������� ����� CompactString()
    // !! Space == ' '
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
      return CompactString(str, /*Space*/ ' ');
    }

    #endregion

    #region replace

    // ����� ��� ������� searchFor � �������� �� replaceTo
    // public static string ReplaceAny(string s, char replaceTo, params char[] searchFor)
    // {
    // if (string.IsNullOrEmpty(s))
    // return s;
    // return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    // }

    // ����� ��� ������� searchFor � �������� �� replaceTo
    // !! ����� ������� � ������� index, � ��������� count
    // public static string ReplaceAny(string s, int index, int count, char replaceTo, params char[] searchFor)
    // {
    // if (s != null && searchFor != null)
    // {
    // int searchForLen = searchFor.Length;
    // if (searchForLen > 0)
    // {
    // if (index < 0)
    // index = 0;
    // int len = index + count;
    // if (len > s.Length)
    // len = s.Length;
    // while (index < len)
    // {
    // char s_char = s[index];
    // int searchForIndex = searchForLen;
    // while (searchForIndex > 0)
    // {
    // if (s_char == searchFor[--searchForIndex])
    // return ReplaceAny(new StringBuilder(s), index, len - index, replaceTo, searchFor).ToString();
    // }
    // ++index;
    // }
    // }
    // }
    // return s;
    // }

    // ����� ��� ������� searchFor � �������� �� replaceTo
    // public static StringBuilder ReplaceAny(StringBuilder s, char replaceTo, params char[] searchFor)
    // {
    // if (IsNullOrEmpty(s))
    // return s;
    // return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    // }

    // ����� ��� ������� searchFor � �������� �� replaceTo
    // !! ����� ������� � ������� index, � ��������� count
    // public static StringBuilder ReplaceAny(StringBuilder s, int index, int count, char replaceTo, params char[] searchFor)
    // {
    // if (s != null && searchFor != null)
    // {
    // int searchForLen = searchFor.Length;
    // if (searchForLen > 0)
    // {
    // if (index < 0)
    // index = 0;
    // int len = index + count;
    // if (len > s.Length)
    // len = s.Length;
    // while (index < len)
    // {
    // char s_char = s[index];
    // int searchForIndex = searchForLen;
    // while (searchForIndex > 0)
    // {
    // if (s_char == searchFor[--searchForIndex])
    // {
    // s[index] = replaceTo;
    // break;
    // }
    // }
    // ++index;
    // }
    // }
    // }
    // return s;
    // }
    #endregion
  }
}