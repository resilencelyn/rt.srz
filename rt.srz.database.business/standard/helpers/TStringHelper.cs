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
        // строка только для чтения
        public struct ReadonlyString
        {
            // конструктор для объекта str
            //public ReadonlyString(str str)
            //{
            //    _string = str;
            //    _stringBuilder = null;
            //}

            // конструктор для объекта str
            //public ReadonlyString(StringBuilder str)
            //{
            //    _string = null;
            //    _stringBuilder = str;
            //}

            // преобразование из объекта str
            //public static implicit operator ReadonlyString(str str)
            //{
            //    return new ReadonlyString(str);
            //}

            // преобразование из объекта str
            //public static implicit operator ReadonlyString(StringBuilder str)
            //{
            //    return new ReadonlyString(str);
            //}

            //---------------------------------------

            // проверка: пустая или нулевая строка
            //public bool IsNullOrEmpty()
            //{
            //    return (_string != null) ? TStringHelper.IsNullOrEmpty(_string) : TStringHelper.IsNullOrEmpty(_stringBuilder);
            //}

            // проверка: нулевая строка или содержащая только пробельные символы
            //public bool IsNullOrWhiteSpace()
            //{
            //    return (_string != null) ? TStringHelper.IsNullOrWhiteSpace(_string) : TStringHelper.IsNullOrWhiteSpace(_stringBuilder);
            //}

            // преобразовать к строке
            //public override string ToString()
            //{
            //    return (_string != null) ? _string : _stringBuilder.ToString();
            //}

            // получить символ в заданной позиции
            //public char this[int index]
            //{
            //    get
            //    {
            //        return (_string != null) ? _string[index] : _stringBuilder[index];
            //    }
            //}

            // длина строки
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

    // IsNullOrEmpty: универсальная перегрузка (для str, StringBuilder...)
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

    // IsNullOrEmpty: универсальная перегрузка (для str, StringBuilder...)
    // public static bool IsNullOrEmpty(StringBuilder str)
    // {
    // return (str == null || str.Length < 1);
    // }

    // проверить, является ли строка нулевой, пустой или содержит только пробельные символы
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

    // перегрузка IsNullOrWhiteSpace()
    // !! для StringBuilder
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

    // преобразовать строку в массив байт
    // !! если строка пуста, возвр. null
    // public static byte[] StringToBytes(string str, bool bUseBigEndian, bool bWriteBOM)
    // {
    // // на пустой строке возвращаем null
    // if (string.IsNullOrEmpty(str))
    // return null;
    // // получаем кодировщик
    // Encoding Encoding = (bUseBigEndian) ? Encoding.BigEndianUnicode : Encoding.Unicode;
    // // кодируем
    // byte[] Result = Encoding.GetBytes(str);
    // // если нужно, включаем BOM-сигнатуру
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
    // // готово
    // return Result;
    // }

    // упрощенный вызов StringToBytes()
    // !! bWriteBOM == false
    // public static byte[] StringToBytes(string str, bool bUseBigEndian)
    // {
    // return StringToBytes(str, bUseBigEndian, /*bWriteBOM*/ false);
    // }

    // упрощенный вызов StringToBytes()
    // !! bWriteBOM == false
    // !! bUseBigEndian == false
    // public static byte[] StringToBytes(string str)
    // {
    // return StringToBytes(str, /*bUseBigEndian*/ false);
    // }

    // преобразовать массив байт в строку
    // !! если массив байт пуст, возвр. null
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
    // // подправим Count
    // if (Count > MaxCount)
    // Count = MaxCount;
    // // убеждаемся, что есть смысл в декодировании
    // if (Count > 0)
    // {
    // // определяем декодировщик
    // Encoding Encoding = null;
    // // анализируем BOM-сигнатуру для Little-Endian формата
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
    // // проверяем совпадение
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
    // // анализируем BOM-сигнатуру для Big-Endian формата
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
    // // проверяем совпадение
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
    // // по умолчанию используем Unicode-Encoding
    // if (Encoding == null)
    // Encoding = Encoding.Unicode;
    // // декодируем
    // return Encoding.GetString(Bytes, (int)Offset, (int)Count);
    // }
    // }
    // }
    // // ошибка
    // return null;
    // }

    // упрощенный вызов StringFromBytes()
    // !! Count берется до конца массива
    // public static string StringFromBytes(byte[] Bytes, long Offset)
    // {
    // return StringFromBytes(Bytes, Offset, /*Count*/ long.MaxValue);
    // }

    // упрощенный вызов StringFromBytes()
    // !! Count берется до конца массива
    // !! Offset == 0
    // public static string StringFromBytes(byte[] Bytes)
    // {
    // return StringFromBytes(Bytes, /*Offset*/ 0);
    // }
    #endregion

    #region strings concatenation

    // сложить несколько строк, выставляя разделители между ними
    // !! последний параметр всегда считается разделителем
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
    // // !! последняя строка - это разделитель
    // string Delimiter = Strings[--StringsCount];
    // bool bHasDelimiter = !string.IsNullOrEmpty(Delimiter);
    // for (long i = 0; i < StringsCount; ++i)
    // {
    // string CurrentString = Strings[i];
    // if (string.IsNullOrEmpty(CurrentString))
    // {
    // // запоминаем первую ненулевую строку !! она может быть просто пустой
    // if (FirstNonNullString == null)
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // если мы впервые встретили непустую строку, просто запоминаем ее
    // if (string.IsNullOrEmpty(FirstNonNullString))
    // {
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // если непустая строка уже есть, нужно создать результат
    // if (ResultString == null)
    // ResultString = new StringBuilder(FirstNonNullString);
    // if (bHasDelimiter)
    // ResultString.Append(Delimiter);
    // ResultString.Append(CurrentString);
    // }
    // }
    // }
    // // если есть несколько объединенных строк, тогда возвращаем их
    // if (ResultString != null)
    // return ResultString.ToString();
    // // в противном случае возвращаем первую же ненулевую строку
    // return FirstNonNullString;
    // }
    // }
    // // нет строк на входе
    // return null;
    // }

    // сложить несколько строк, выставляя разделители между ними
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
    // // запоминаем первую ненулевую строку !! она может быть просто пустой
    // if (FirstNonNullString == null)
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // если мы впервые встретили непустую строку, просто запоминаем ее
    // if (string.IsNullOrEmpty(FirstNonNullString))
    // {
    // FirstNonNullString = CurrentString;
    // }
    // else
    // {
    // // если непустая строка уже есть, нужно создать результат
    // if (ResultString == null)
    // ResultString = new StringBuilder(FirstNonNullString);
    // if (bHasDelimiter)
    // ResultString.Append(Delimiter);
    // ResultString.Append(CurrentString);
    // }
    // }
    // }
    // // если есть несколько объединенных строк, тогда возвращаем их
    // if (ResultString != null)
    // return ResultString.ToString();
    // // в противном случае возвращаем первую же ненулевую строку
    // return FirstNonNullString;
    // }
    // // нет строк на входе
    // return null;
    // }

    // сложить две строки
    // !! если обе строки не пусты, между ними ставится заданный разделитель
    // public static string CombineStrings(string LeftString, string RightString, string Delimiter)
    // {
    // return CombineMultipleStrings(LeftString, RightString, Delimiter);
    // }

    // упрощенный вызов CombineStrings()
    // !! Delimiter == " "
    // public static string CombineStrings(string LeftString, string RightString)
    // {
    // return CombineStrings(LeftString, RightString, /*Delimiter*/ " ");
    // }

    // добавить строку к имеющемуся тексту
    // !! если Builder == null, он создается при первом вызове
    // !! если в билдере есть текст, тогда перед строкой ставится заданный разделитель; если текста нет, то строка просто вставляется в билдер
    // !! если str пустая или нулевая, ничего не происходит
    // <returns>возвращает билдер, полученный на входе</returns>
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
    // // готово
    // return Builder;
    // }

    // упрощенный вызов CombineStrings()
    // !! Delimiter == " "
    // public static StringBuilder CombineStrings(StringBuilder Builder, string str)
    // {
    // return CombineStrings(Builder, str, /*Delimiter*/ " ");
    // }
    #endregion

    #region str <-> StringBuilder

    // преобразовать str в StringBuilder
    // !! если на входе null, возвращает также null
    // public static StringBuilder StringToStringBuilder(str s)
    // {
    // if (s == null)
    // return null;
    // return new StringBuilder(s);
    // }

    // преобразовать StringBuilder в str
    // !! если на входе null, возвращает также null
    // public static string StringBuilderToString(StringBuilder sb)
    // {
    // if (sb == null)
    // return null;
    // return sb.ToString();
    // }
    #endregion

    #region empty strings

    // привести строку к заданному виду, если она ПУСТАЯ
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
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

    // упрощенная версия StringShrink()
    // !! bTrimFirst == false
    // public static string StringShrink(string str, TEmptyStringTypes EmptyType)
    // {
    // return StringShrink(str, EmptyType, /*bTrimFirst*/ false);
    // }

    // привести строку к null, если она пустая
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
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

    // упрощенная версия StringToNull()
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

    // возвратить string.Empty, если на входе пустая строка (в т.ч. null)
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
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

    // упрощенная версия StringToEmpty()
    // !! bTrimFirst == false
    // public static string StringToEmpty(string str)
    // {
    // return StringToEmpty(str, /*bTrimFirst*/ false);
    // }

    // преобразование строки к формату XML
    // public static string ConvertToXmlString(string str, bool bDoubleApostrophe)
    // {
    // return str.Replace("&", "&amp;").Replace("'", (bDoubleApostrophe) ? "&apos;&apos;" : "&apos;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    // }

    // упрощенная версия ConvertToXmlString()
    // !! bDoubleApostrophe == false
    // public static string ConvertToXmlString(string str)
    // {
    // return ConvertToXmlString(str, /*bDoubleApostrophe*/ false);
    // }
    #endregion

    #region empty strings для StringBuilder

    // привести строку к заданному виду, если она ПУСТАЯ
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
    // public static string StringShrink(StringBuilder str, TEmptyStringTypes EmptyType, bool bTrimFirst)
    // {
    // return StringShrink(StringBuilderToString(str), EmptyType, bTrimFirst);
    // }

    // упрощенная версия StringShrink()
    // !! bTrimFirst == false
    // public static string StringShrink(StringBuilder str, TEmptyStringTypes EmptyType)
    // {
    // return StringShrink(str, EmptyType, /*bTrimFirst*/ false);
    // }

    // привести строку к null, если она пустая
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
    // public static string StringToNull(StringBuilder str, bool bTrimFirst)
    // {
    // return StringShrink(str, TEmptyStringTypes.Null, bTrimFirst);
    // }

    // упрощенная версия StringToNull()
    // !! bTrimFirst == false
    // public static string StringToNull(StringBuilder str)
    // {
    // return StringToNull(str, /*bTrimFirst*/ false);
    // }

    // возвратить string.Empty, если на входе пустая строка (в т.ч. null)
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
    // public static string StringToEmpty(StringBuilder str, bool bTrimFirst)
    // {
    // return StringShrink(str, TEmptyStringTypes.Empty, bTrimFirst);
    // }

    // упрощенная версия StringToEmpty()
    // !! bTrimFirst == false
    // public static string StringToEmpty(StringBuilder str)
    // {
    // return StringToEmpty(str, /*bTrimFirst*/ false);
    // }
    #endregion

    #region string shrink

    // обрезать строку ТОЛЬКО в том случае, если ее длина превышает заданную
    // public static string ShrinkToLength(string str, int MaxLength)
    // {
    // if (!IsNullOrEmpty(str))
    // {
    // if (str.Length > MaxLength)
    // return str.Substring(0, (MaxLength > 0) ? MaxLength : 0);
    // }
    // return str;
    // }

    // версия ShrinkToLength() для StringBuilder
    // public static StringBuilder ShrinkToLength(StringBuilder str, int MaxLength)
    // {
    // if (!IsNullOrEmpty(str))
    // {
    // if (str.Length > MaxLength)
    // str.Length = (MaxLength > 0) ? MaxLength : 0;
    // }
    // return str;
    // }

    // уплотнить строку, заменяя все множественные пробелы/табуляции/и т.п. на заданный символ
    // !! можно задать Space = '\0', чтобы схлопнуть "в ноль"
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
              // оптимизация: если пробел только один и он равен заданному Space-символу, ничего делать не надо
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

// если что-то заменяли, возвращаем полученную строку
        if (result != null)
        {
          if (start < len)
            result.Append(str.Substring(start));
          return result.ToString();
        }
      }

      return str;
    }

    // упрощенный вызов CompactString()
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

    // найти все символы searchFor и заменить на replaceTo
    // public static string ReplaceAny(string s, char replaceTo, params char[] searchFor)
    // {
    // if (string.IsNullOrEmpty(s))
    // return s;
    // return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    // }

    // найти все символы searchFor и заменить на replaceTo
    // !! поиск ведется с позиции index, в диапазоне count
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

    // найти все символы searchFor и заменить на replaceTo
    // public static StringBuilder ReplaceAny(StringBuilder s, char replaceTo, params char[] searchFor)
    // {
    // if (IsNullOrEmpty(s))
    // return s;
    // return ReplaceAny(s, 0, s.Length, replaceTo, searchFor);
    // }

    // найти все символы searchFor и заменить на replaceTo
    // !! поиск ведется с позиции index, в диапазоне count
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