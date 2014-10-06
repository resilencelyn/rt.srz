// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterHelper.cs" company="��������">
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
    // ������ ������ ����/�������
    #region Static Fields

    /// <summary>
    ///   The date time format.
    /// </summary>
    public static readonly string DateTimeFormat = @"yyyy'/'MM'/'dd' 'HH':'mm':'ss'.'fff";

    // ������� ����/�����
    /// <summary>
    ///   The date time zero.
    /// </summary>
    public static readonly DateTime DateTimeZero = new DateTime();

    #endregion

    // ������������� �� ������
    // !! �� ������ �������������� ���������� ���������� (� ��� �����, ���� type == Undefined)
    // public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, IList<string> formats)
    // {
    // switch (type)
    // {
    // // ������
    // case CastType.String:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return string.Empty;
    // return strValue;
    // }
    // // ����/�����
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
    // // ����� �����������
    // case CastType.Numeric:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<UInt64>();
    // return UInt64.Parse(strValue);
    // }
    // // ����� ��������
    // case CastType.Signed:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<Int64>();
    // return Int64.Parse(strValue);
    // }
    // // ��������������
    // case CastType.Real:
    // {
    // if (allowEmptyValue && string.IsNullOrEmpty(strValue))
    // return new Nullable<Double>();
    // return Double.Parse(strValue);
    // }
    // }
    // throw new InvalidCastException(string.Format("����������� ��� ��������������: {0}", type));
    // }

    // ������������� �� ������
    // !! �� ������ �������������� ���������� ���������� (� ��� �����, ���� type == Undefined)
    // public static object CastFromString(CastType type, string strValue, IList<string> formats)
    // {
    // return CastFromString(type, strValue, /*allowEmptyValue*/ false, formats);
    // }

    // ������������� �� ������
    // !! �� ������ �������������� ���������� ���������� (� ��� �����, ���� type == Undefined)
    // public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, params string[] formats)
    // {
    // return CastFromString(type, strValue, allowEmptyValue, (IList<string>)formats);
    // }

    // ������������� �� ������
    // !! �� ������ �������������� ���������� ���������� (� ��� �����, ���� type == Undefined)
    // public static object CastFromString(CastType type, string strValue, params string[] formats)
    // {
    // return CastFromString(type, strValue, (IList<string>)formats);
    // }

    // ������������� �������������� �������
    // !! �� ������ ���������� ����������
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
    // // ��������� Nullable<>
    // Type nullableType = Nullable.GetUnderlyingType(type);
    // if (nullableType != null)
    // {
    // // !! �� ����� ������� ������� ������ Nullable<T>
    // // !! ������� ���������� ������ T, ���� null
    // if (o == null)
    // return o;
    // type = nullableType;
    // }
    // return Convert.ChangeType(o, type);
    // }

    // ������������� �������������� �������
    // !! �� ������ ���������� ����������
    // public static T ConvertToValue<T>(object o)
    // {
    // return (T)ConvertToValue(typeof(T), o);
    // }

    // ������������� �������������� �� ������
    // !! �� ������ ���������� ����������
    // public static T StringToValue<T>(string strValue)
    // {
    // return ConvertToValue<T>(strValue);
    // }

    // �������� bool �� ������
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
          case "��":
            return BooleanFlag.True;
          case "FALSE":
          case "���":
          case "0":
            return BooleanFlag.False;
        }
      }

      return BooleanFlag.Unknown;
    }

    #endregion

    // �������������� �� ������ � TimeSpan
    // !! �� ������ ���������� ����������
    // public static TimeSpan StringToTimeSpan(string strValue, TimePart timePart)
    // {
    // double value = StringToValue<double>(strValue);
    // return DoubleToTimeSpan(value, timePart);
    // }

    // �������������� �� double � TimeSpan
    // !! �� ������ ���������� ����������
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

    // �������������� �� TimeSpan � double
    // !! �� ������ ���������� ����������
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

    // �������������� TimeSpan � ������
    // public static string TimeSpanToString(TimeSpan value, TimePart timePart)
    // {
    // return TimeSpanToDouble(value, timePart).ToString();
    // }

    // ������������� ����/����� � ������
    // public static string DateTimeToString(DateTime value, string format)
    // {
    // return DateTime.SpecifyKind(value, DateTimeKind.Unspecified).ToString(format);
    // }

    // ������������� ����/����� � ������
    // !! ��� ������� ���� ��������� ������ ������
    // public static string DateTimeToString(DateTime? value, string format)
    // {
    // if (value.HasValue)
    // return DateTimeToString(value.Value, format);
    // return string.Empty;
    // }

    // �������������� ����/������� � ������ ��� ������������� ������������
    // public static string DateTimeAsUserString(DateTime value, bool localTime = true)
    // {
    // if (localTime)
    // value = value.ToLocalTime();
    // return value.ToString(DateTimeFormat);
    // }

    // �������������� ����/������� � ������ ��� ������������� ������������
    // public static string DateTimeAsUserString(DateTime? value, bool localTime = true, string nullText = "����������")
    // {
    // if (value.HasValue)
    // return DateTimeAsUserString(value.Value, localTime);
    // return nullText;
    // }

    // �������������� ������� � ������ ��� ������������� ������������
    // public static string TimeSpanAsUserString(TimeSpan t)
    // {
    // // ������ ����
    // if (t.TotalMilliseconds <= 1)
    // return "1 ����";
    // // �� �������
    // if (t.TotalMilliseconds < 1000)
    // return string.Format("{0} ����", Math.Round(t.TotalMilliseconds));
    // // �� ������
    // if (t.TotalSeconds < 60)
    // return string.Format("{0} ���", t.TotalSeconds);
    // StringBuilder result = new StringBuilder();
    // // �����
    // var number = t.TotalDays;
    // if (number >= 1)
    // {
    // var floor = Math.Floor(number);
    // result.AppendFormat("{0} �����, ", (int)floor);
    // number = 24 * (number - floor);
    // }
    // else
    // number *= 24;
    // // ����
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
    // // ������
    // if (number >= 1)
    // {
    // var floor = Math.Floor(number);
    // result.AppendFormat("{0:00}:", (int)floor);
    // number = 60 * (number - floor);
    // }
    // else
    // number *= 60;
    // // �������
    // result.AppendFormat("{0:00}", (int)Math.Floor(number));
    // // ������
    // return result.ToString();
    // }

    // �������������� ������� � ������ ��� ������������� ������������
    // public static string TimeSpanAsUserString(TimeSpan? t, string nullText = "����������")
    // {
    // if (t.HasValue)
    // return TimeSpanAsUserString(t.Value);
    // return nullText;
    // }

    // �������� ������������������ ("������ �� �����") � ���� ������ ��� ������������� ������������
    // public static string TimeRatioAsUserString(TimeSpan interval, double units, string unitName, string undefinedText = "����������")
    // {
    // TimePart timePart;
    // double ratio = DoOptimizeTimeRatio(interval, units, out timePart);
    // if (double.IsNaN(ratio) || double.IsInfinity(ratio))
    // return undefinedText;
    // string timePartName;
    // switch (timePart)
    // {
    // case TimePart.Milliseconds:
    // timePartName = "����";
    // break;
    // case TimePart.Seconds:
    // timePartName = "���";
    // break;
    // case TimePart.Minutes:
    // timePartName = "���";
    // break;
    // case TimePart.Hours:
    // timePartName = "���";
    // break;
    // case TimePart.Days:
    // timePartName = "�����";
    // break;
    // default:
    // return undefinedText;
    // }
    // return string.Format("{0} {1}/{2}", DoubleAsUserString(ratio), unitName, timePartName);
    // }

    // �������������� ��������������� �������� � ������ ��� ������������� ������������
    // public static string DoubleAsUserString(double value)
    // {
    // return value.ToString(value < 0.001 ? "E3" : "F3");
    // }

    // ����������� ������������� ���������� ������
    // !! �� ������ ���������� null
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

    // ��������� ����� ���� � ������ � ������������������ �������
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

    // ������ �������� ������ �������, ������� � �������� �������
    // public static int SearchQuote(this string str, ref QuotationMark quote, int startIndex = 0)
    // {
    // if (str == null || startIndex < 0)
    // throw new ArgumentException("�� ���� ������ ������� � ��������� ����������� ������");
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

    // �������� ��������� ��� TimeSpan
    // public static TimeSpan Multiply(this TimeSpan value, double multiplier)
    // {
    // return TimeSpan.FromTicks(unchecked((long)(value.Ticks * multiplier)));
    // }

    // �������� ��������� ��� TimeSpan
    // public static TimeSpan Multiply(this TimeSpan value, long multiplier)
    // {
    // return TimeSpan.FromTicks(value.Ticks * multiplier);
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static Byte Between(this Byte value, Byte min, Byte max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // [CLSCompliant(false)]
    // public static SByte Between(this SByte value, SByte min, SByte max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static Int16 Between(this Int16 value, Int16 min, Int16 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // [CLSCompliant(false)]
    // public static UInt16 Between(this UInt16 value, UInt16 min, UInt16 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static Int32 Between(this Int32 value, Int32 min, Int32 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // [CLSCompliant(false)]
    // public static UInt32 Between(this UInt32 value, UInt32 min, UInt32 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static Int64 Between(this Int64 value, Int64 min, Int64 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // [CLSCompliant(false)]
    // public static UInt64 Between(this UInt64 value, UInt64 min, UInt64 max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static Decimal Between(this Decimal value, Decimal min, Decimal max)
    // {
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
    // public static float Between(this float value, float min, float max)
    // {
    // if (float.IsNaN(value))
    // return min;
    // return Math.Min(max, Math.Max(min, value));
    // }

    // ������� ��������, �� ��������� �� ������� [min, max]
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
    // // ���� ����� ������ ������ 1, �������� � ����� � ������� �� 10, ���� �� ��������� �����, �������� 1
    // while (units < 1)
    // {
    // // ��������� �� �������� �������� � ������, ��������� ������� >= �����, �� ������ ������� ���������
    // var totalDays = interval.TotalDays;
    // if (totalDays >= 1)
    // {
    // timePart = TimePart.Days;
    // return units / totalDays;
    // }
    // interval = interval.Multiply(10);
    // units *= 10;
    // }
    // // �������� ����� ������ � ������������
    // timePart = TimePart.Milliseconds;
    // double ratio = units / interval.TotalMilliseconds;
    // // ������� ��������� � ��������� �����������, ���� ratio < 1
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
    // // ��������� �����, ���������� ����������
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
    // throw new InvalidCastException(string.Format("����������� ��� ��������������: {0}", timePart.ToString()));
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