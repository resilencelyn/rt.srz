// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterHelper.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The conversion helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.helpers
{
  using System;

  using rt.srz.database.business.standard.enums;

  /// <summary>
  ///   The conversion helper.
  /// </summary>
  public static class ConversionHelper
  {
    // формат вывода даты/времени
    #region Static Fields

    /// <summary>
    ///   The date time format.
    /// </summary>
    public static readonly string DateTimeFormat = @"yyyy'/'MM'/'dd' 'HH':'mm':'ss'.'fff";

    // нулевые дата/время
    /// <summary>
    ///   The date time zero.
    /// </summary>
    public static readonly DateTime DateTimeZero = new DateTime();

    #endregion

    // преобразовать из строки
    // !! на ошибке преобразования выкидывает исключение (в том числе, если type == Undefined)
    // public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, IList<string> formats)
    // {
    // switch (type)
    // {
    // // строка
    // case CastType.String:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return string.Empty;
    // return strValue;
    // }
    // // дата/время
    // case CastType.DateTime:
    // case CastType.Date:
    // case CastType.Time:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<DateTime>();
    // DateTime value = DoParseDateTime(strValue, formats);
    // switch (type)
    // {
    // case CastType.Date:
    // return value.Date;
    // case CastType.Time:
    // return value.TimeOfDay;
    // }
    // return value;
    // }
    // // целое беззнаковое
    // case CastType.Numeric:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<UInt64>();
    // return UInt64.Parse(strValue);
    // }
    // // целое знаковое
    // case CastType.Signed:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<Int64>();
    // return Int64.Parse(strValue);
    // }
    // // действительное
    // case CastType.Real:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<Double>();
    // return Double.Parse(strValue);
    // }
    // }
    // throw new InvalidCastException(string.Format("Неизвестный тип преобразования: {0}", type));
    // }

    // преобразовать из строки
    // !! на ошибке преобразования выкидывает исключение (в том числе, если type == Undefined)
    // public static object CastFromString(CastType type, string strValue, IList<string> formats)
    // {
    // return CastFromString(type, strValue, /*allowEmptyValue*/ false, formats);
    // }

    // преобразовать из строки
    // !! на ошибке преобразования выкидывает исключение (в том числе, если type == Undefined)
    // public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, params string[] formats)
    // {
    // return CastFromString(type, strValue, allowEmptyValue, (IList<string>)formats);
    // }

    // преобразовать из строки
    // !! на ошибке преобразования выкидывает исключение (в том числе, если type == Undefined)
    // public static object CastFromString(CastType type, string strValue, params string[] formats)
    // {
    // return CastFromString(type, strValue, (IList<string>)formats);
    // }

    // универсальное преобразование объекта
    // !! на ошибке выкидывает исключение
    // public static object ConvertToValue(Type type, object o)
    // {
    // if (o != null && o.GetType() == type)
    // return o;
    // if (typeof(Enum).IsAssignableFrom(type))
    // {
    // string s = o as string;
    // if (s != null)
    // return Enum.Parse(type, s);
    // return Enum.ToObject(type, o);
    // }
    // // отработка Nullable<>
    // Type nullableType = Nullable.GetUnderlyingType(type);
    // if (nullableType != null)
    // {
    // // !! не нашел способа создать объект Nullable<T>
    // // !! поэтому возвращаем просто T, либо null
    // if (o == null)
    // return o;
    // type = nullableType;
    // }
    // return Convert.ChangeType(o, type);
    // }

    // универсальное преобразование объекта
    // !! на ошибке выкидывает исключение
    // public static T ConvertToValue<T>(object o)
    // {
    // return (T)ConvertToValue(typeof(T), o);
    // }

    // универсальное преобразование из строки
    // !! на ошибке выкидывает исключение
    // public static T StringToValue<T>(string strValue)
    // {
    // return ConvertToValue<T>(strValue);
    // }

    // получить bool из строки
    #region Public Methods and Operators

    /// <summary>
    /// The string to bool.
    /// </summary>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <returns>
    /// The <see cref="BooleanFlag"/>.
    /// </returns>
    public static BooleanFlag StringToBool(string strValue)
    {
      if (strValue != null)
      {
        switch (strValue.ToUpper())
        {
          case "1":
          case "TRUE":
          case "ДА":
            return BooleanFlag.True;
          case "FALSE":
          case "НЕТ":
          case "0":
            return BooleanFlag.False;
        }
      }

      return BooleanFlag.Unknown;
    }

    #endregion

    // преобразование из строки в TimeSpan
    // !! на ошибке выкидывает исключение
    // public static TimeSpan StringToTimeSpan(string strValue, TimePart timePart)
    // {
    // double value = StringToValue<double>(strValue);
    // return DoubleToTimeSpan(value, timePart);
    // }

    // преобразование из double в TimeSpan
    // !! на ошибке выкидывает исключение
    // public static TimeSpan DoubleToTimeSpan(double value, TimePart timePart)
    // {
    // switch (timePart)
    // {
    // case TimePart.Milliseconds:
    // return TimeSpan.FromMilliseconds(value);
    // case TimePart.Seconds:
    // return TimeSpan.FromSeconds(value);
    // case TimePart.Minutes:
    // return TimeSpan.FromMinutes(value);
    // case TimePart.Hours:
    // return TimeSpan.FromHours(value);
    // case TimePart.Days:
    // return TimeSpan.FromDays(value);
    // }
    // ThrowTimeSpanConversionError(timePart);
    // return TimeSpan.Zero;
    // }

    // преобразование из TimeSpan в double
    // !! на ошибке выкидывает исключение
    // public static double TimeSpanToDouble(TimeSpan value, TimePart timePart)
    // {
    // switch (timePart)
    // {
    // case TimePart.Milliseconds:
    // return value.TotalMilliseconds;
    // case TimePart.Seconds:
    // return value.TotalSeconds;
    // case TimePart.Minutes:
    // return value.TotalMinutes;
    // case TimePart.Hours:
    // return value.TotalHours;
    // case TimePart.Days:
    // return value.TotalDays;
    // }
    // ThrowTimeSpanConversionError(timePart);
    // return double.NaN;
    // }

    // преобразование TimeSpan в строку
    // public static string TimeSpanToString(TimeSpan value, TimePart timePart)
    // {
    // return TimeSpanToDouble(value, timePart).ToString();
    // }

    // преобразовать дату/время в строку
    // public static string DateTimeToString(DateTime value, string format)
    // {
    // return DateTime.SpecifyKind(value, DateTimeKind.Unspecified).ToString(format);
    // }

    // преобразовать дату/время в строку
    // !! для нулевой даты возращает пустую строку
    // public static string DateTimeToString(DateTime? value, string format)
    // {
    // if (value.HasValue)
    // return DateTimeToString(value.Value, format);
    // return string.Empty;
    // }

    // преобразование даты/времени в строку для представления пользователю
    // public static string DateTimeAsUserString(DateTime value, bool localTime = true)
    // {
    // if (localTime)
    // value = value.ToLocalTime();
    // return value.ToString(DateTimeFormat);
    // }

    // преобразование даты/времени в строку для представления пользователю
    // public static string DateTimeAsUserString(DateTime? value, bool localTime = true, string nullText = "неизвестно")
    // {
    // if (value.HasValue)
    // return DateTimeAsUserString(value.Value, localTime);
    // return nullText;
    // }

    // преобразование времени в строку для представления пользователю
    // public static string TimeSpanAsUserString(TimeSpan t)
    // {
    // // совсем мало
    // if (t.TotalMilliseconds <= 1)
    // return "1 мсек";
    // // до секунды
    // if (t.TotalMilliseconds < 1000)
    // return string.Format("{0} мсек", Math.Round(t.TotalMilliseconds));
    // // до минуты
    // if (t.TotalSeconds < 60)
    // return string.Format("{0} сек", t.TotalSeconds);
    // StringBuilder result = new StringBuilder();
    // // сутки
    // var number = t.TotalDays;
    // if (number >= 1)
    // {
    // var floor = Math.Floor(number);
    // result.AppendFormat("{0} суток, ", (int)floor);
    // number = 24 * (number - floor);
    // }
    // else
    // number *= 24;
    // // часы
    // if (number >= 1)
    // {
    // var floor = Math.Floor(number);
    // result.AppendFormat("{0:00}:", (int)floor);
    // number = 60 * (number - floor);
    // }
    // else
    // {
    // result.Append("00:");
    // number *= 60;
    // }
    // // минуты
    // if (number >= 1)
    // {
    // var floor = Math.Floor(number);
    // result.AppendFormat("{0:00}:", (int)floor);
    // number = 60 * (number - floor);
    // }
    // else
    // number *= 60;
    // // секунды
    // result.AppendFormat("{0:00}", (int)Math.Floor(number));
    // // готово
    // return result.ToString();
    // }

    // преобразование времени в строку для представления пользователю
    // public static string TimeSpanAsUserString(TimeSpan? t, string nullText = "неизвестно")
    // {
    // if (t.HasValue)
    // return TimeSpanAsUserString(t.Value);
    // return nullText;
    // }

    // получить производительность ("единиц за время") в виде строки для представления пользователю
    // public static string TimeRatioAsUserString(TimeSpan interval, double units, string unitName, string undefinedText = "неизвестно")
    // {
    // TimePart timePart;
    // double ratio = DoOptimizeTimeRatio(interval, units, out timePart);
    // if (double.IsNaN(ratio) || double.IsInfinity(ratio))
    // return undefinedText;
    // string timePartName;
    // switch (timePart)
    // {
    // case TimePart.Milliseconds:
    // timePartName = "мсек";
    // break;
    // case TimePart.Seconds:
    // timePartName = "сек";
    // break;
    // case TimePart.Minutes:
    // timePartName = "мин";
    // break;
    // case TimePart.Hours:
    // timePartName = "час";
    // break;
    // case TimePart.Days:
    // timePartName = "сутки";
    // break;
    // default:
    // return undefinedText;
    // }
    // return string.Format("{0} {1}/{2}", DoubleAsUserString(ratio), unitName, timePartName);
    // }

    // преобразование действительного значения в строку для представления пользователю
    // public static string DoubleAsUserString(double value)
    // {
    // return value.ToString(value < 0.001 ? "E3" : "F3");
    // }

    // попробовать преобразовать версионную строку
    // !! на ошибке возвращает null
    // public static Version TryStringToVersion(string strValue)
    // {
    // if (!string.IsNullOrEmpty(strValue))
    // {
    // var numbers = strValue.Split('.');
    // var numbersLength = numbers.Length;
    // if (numbersLength <= 4)
    // {
    // int[] ver = new int[4];
    // int i = 0;
    // for (; i < numbersLength; ++i)
    // {
    // if (!int.TryParse(numbers[i], out ver[i]))
    // return null;
    // }
    // while (i < 4)
    // ver[i++] = 0;
    // return new Version(ver[0], ver[1], ver[2], ver[3]);
    // }
    // }
    // return null;
    // }

    // перевести набор байт в строку с шестнадцитиричными числами
    // public static string BytesToHexString(byte[] bytes)
    // {
    // StringBuilder sb = null;
    // if (bytes != null)
    // {
    // foreach (byte b in bytes)
    // sb = TStringHelper.CombineStrings(sb, b.ToString("x2").ToUpper(), Delimiter: null);
    // }
    // return TStringHelper.StringToEmpty(sb);
    // }

    // искать заданный символ кавычки, начиная с заданной позиции
    // public static int SearchQuote(this string str, ref QuotationMark quote, int startIndex = 0)
    // {
    // if (str == null || startIndex < 0)
    // throw new ArgumentException("не могу искать кавычки с заданными параметрами поиска");
    // int result;
    // switch (quote)
    // {
    // case QuotationMark.Apostrophe:
    // result = str.IndexOf('\'', startIndex);
    // break;
    // case QuotationMark.DoubleQuotes:
    // result = str.IndexOf('\"', startIndex);
    // break;
    // default:
    // result = str.IndexOfAny(quoutes, startIndex);
    // break;
    // }
    // if (result >= 0)
    // {
    // switch (str[result])
    // {
    // case '\'':
    // quote = QuotationMark.Apostrophe;
    // break;
    // case '\"':
    // quote = QuotationMark.DoubleQuotes;
    // break;
    // }
    // }
    // else
    // quote = QuotationMark.None;
    // return result;
    // }

    // операция умножения для TimeSpan
    // public static TimeSpan Multiply(this TimeSpan value, double multiplier)
    // {
    // return TimeSpan.FromTicks(unchecked((long)(value.Ticks * multiplier)));
    // }

    // операция умножения для TimeSpan
    // public static TimeSpan Multiply(this TimeSpan value, long multiplier)
    // {
    // return TimeSpan.FromTicks(value.Ticks * multiplier);
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static Byte Between(this Byte value, Byte min, Byte max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // [CLSCompliant(false)]
    // public static SByte Between(this SByte value, SByte min, SByte max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static Int16 Between(this Int16 value, Int16 min, Int16 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // [CLSCompliant(false)]
    // public static UInt16 Between(this UInt16 value, UInt16 min, UInt16 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static Int32 Between(this Int32 value, Int32 min, Int32 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // [CLSCompliant(false)]
    // public static UInt32 Between(this UInt32 value, UInt32 min, UInt32 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static Int64 Between(this Int64 value, Int64 min, Int64 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // [CLSCompliant(false)]
    // public static UInt64 Between(this UInt64 value, UInt64 min, UInt64 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static Decimal Between(this Decimal value, Decimal min, Decimal max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static float Between(this float value, float min, float max)
    // {
    // if (float.IsNaN(value))
    // return min;
    // return Math.Min(max, Math.Max(min, value));
    // }

    // вернуть значение, не выходящее за пределы [min, max]
    // public static double Between(this double value, double min, double max)
    // {
    // if (double.IsNaN(value))
    // return min;
    // return Math.Min(max, Math.Max(min, value));
    // }

    // --------------------------------------------------------

    // static DateTime DoParseDateTime(string strValue, IList<string> formats)
    // {
    // if (formats != null && formats.Count > 0)
    // {
    // DateTime value;
    // foreach (var format in formats)
    // {
    // if (DateTime.TryParseExact(strValue, format, /*IFormatProvider*/ null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
    // return value;
    // }
    // }
    // return DateTime.Parse(strValue, /*IFormatProvider*/ null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
    // }

    // --------------------------------------------------------

    // static double DoOptimizeTimeRatio(TimeSpan interval, double units, out TimePart timePart)
    // {
    // if (interval.TotalMilliseconds >= 1 && units > 0)
    // {
    // // если число единиц меньше 1, умножаем и время и единицы на 10, пока не достигнем числа, большего 1
    // while (units < 1)
    // {
    // // поскольку мы измеряем максимум в сутках, достигнув времени >= суток, мы просто выдадим результат
    // var totalDays = interval.TotalDays;
    // if (totalDays >= 1)
    // {
    // timePart = TimePart.Days;
    // return units / totalDays;
    // }
    // interval = interval.Multiply(10);
    // units *= 10;
    // }
    // // получаем число единиц в миллисекунду
    // timePart = TimePart.Milliseconds;
    // double ratio = units / interval.TotalMilliseconds;
    // // пробуем перевести в следующую размерность, пока ratio < 1
    // while (ratio < 1)
    // {
    // switch (timePart)
    // {
    // case TimePart.Milliseconds:
    // ratio *= 1000;
    // timePart = TimePart.Seconds;
    // break;
    // case TimePart.Seconds:
    // ratio *= 60;
    // timePart = TimePart.Minutes;
    // break;
    // case TimePart.Minutes:
    // ratio *= 60;
    // timePart = TimePart.Hours;
    // break;
    // case TimePart.Hours:
    // ratio *= 24;
    // timePart = TimePart.Days;
    // break;
    // }
    // // достигнув суток, прекращаем увеличение
    // if (timePart == TimePart.Days)
    // break;
    // }
    // return ratio;
    // }
    // timePart = TimePart.Undefined;
    // return double.NaN;
    // }

    // --------------------------------------------------------

    // static void ThrowTimeSpanConversionError(TimePart timePart)
    // {
    // throw new InvalidCastException(string.Format("Неизвестный тип преобразования: {0}", timePart.ToString()));
    // }

    // --------------------------------------------------------

    // static char[] quoutes = { '\'', '\"' };

    // --------------------------------------------------------

    // --------------------------------------------------------

    // --------------------------------------------------------

    // --------------------------------------------------------

    // --------------------------------------------------------
  }
}