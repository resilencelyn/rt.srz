// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversionHelper.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The conversion helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Text;

  using rt.srz.model.Hl7.commons.Enumerations;
  using rt.srz.model.Hl7.dotNetX;

  #endregion

  /// <summary>
  ///   The conversion helper.
  /// </summary>
  public static class ConversionHelper
  {
    #region Static Fields

    /// <summary>
    ///   The date time format.
    /// </summary>
    public static readonly string DateTimeFormat = "yyyy'/'MM'/'dd' 'HH':'mm':'ss'.'fff";

    /// <summary>
    ///   The date time zero.
    /// </summary>
    public static readonly DateTime DateTimeZero = new DateTime();

    /// <summary>
    ///   The quoutes.
    /// </summary>
    private static readonly char[] quoutes = { '\'', '"' };

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="byte"/>.
    /// </returns>
    public static byte Between(this byte value, byte min, byte max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="decimal"/>.
    /// </returns>
    public static decimal Between(this decimal value, decimal min, decimal max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    public static double Between(this double value, double min, double max)
    {
      if (double.IsNaN(value))
      {
        return min;
      }

      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="short"/>.
    /// </returns>
    public static short Between(this short value, short min, short max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public static int Between(this int value, int min, int max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="long"/>.
    /// </returns>
    public static long Between(this long value, long min, long max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="sbyte"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static sbyte Between(this sbyte value, sbyte min, sbyte max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="float"/>.
    /// </returns>
    public static float Between(this float value, float min, float max)
    {
      if (float.IsNaN(value))
      {
        return min;
      }

      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="ushort"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static ushort Between(this ushort value, ushort min, ushort max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="uint"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static uint Between(this uint value, uint min, uint max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The between.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <returns>
    /// The <see cref="ulong"/>.
    /// </returns>
    [CLSCompliant(false)]
    public static ulong Between(this ulong value, ulong min, ulong max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    /// <summary>
    /// The bytes to hex string.
    /// </summary>
    /// <param name="bytes">
    /// The bytes.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string BytesToHexString(byte[] bytes)
    {
      StringBuilder builder = null;
      if (bytes != null)
      {
        foreach (var num in bytes)
        {
          string delimiter = null;
          builder = TStringHelper.CombineStrings(builder, num.ToString("x2").ToUpper(), delimiter);
        }
      }

      return TStringHelper.StringToEmpty(builder);
    }

    /// <summary>
    /// The cast from string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="formats">
    /// The formats.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object CastFromString(CastType type, string strValue, IList<string> formats)
    {
      return CastFromString(type, strValue, false, formats);
    }

    /// <summary>
    /// The cast from string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="formats">
    /// The formats.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object CastFromString(CastType type, string strValue, params string[] formats)
    {
      return CastFromString(type, strValue, (IList<string>)formats);
    }

    /// <summary>
    /// The cast from string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="allowEmptyValue">
    /// The allow empty value.
    /// </param>
    /// <param name="formats">
    /// The formats.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, params string[] formats)
    {
      return CastFromString(type, strValue, allowEmptyValue, (IList<string>)formats);
    }

    /// <summary>
    /// The cast from string.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="allowEmptyValue">
    /// The allow empty value.
    /// </param>
    /// <param name="formats">
    /// The formats.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    /// <exception cref="InvalidCastException">
    /// </exception>
    public static object CastFromString(CastType type, string strValue, bool allowEmptyValue, IList<string> formats)
    {
      switch (type)
      {
        case CastType.String:
          if (!allowEmptyValue || !string.IsNullOrEmpty(strValue))
          {
            return strValue;
          }

          return string.Empty;

        case CastType.DateTime:
        case CastType.Date:
        case CastType.Time:
          if (!allowEmptyValue || !string.IsNullOrEmpty(strValue))
          {
            var time = DoParseDateTime(strValue, formats);
            switch (type)
            {
              case CastType.Date:
                return time.Date;

              case CastType.Time:
                return time.TimeOfDay;
            }

            return time;
          }

          return null;

        case CastType.Numeric:
          if (!allowEmptyValue || !string.IsNullOrEmpty(strValue))
          {
            return ulong.Parse(strValue);
          }

          return null;

        case CastType.Signed:
          if (!allowEmptyValue || !string.IsNullOrEmpty(strValue))
          {
            return long.Parse(strValue);
          }

          return null;

        case CastType.Real:
          if (!allowEmptyValue || !string.IsNullOrEmpty(strValue))
          {
            return double.Parse(strValue);
          }

          return null;
      }

      throw new InvalidCastException(string.Format("Неизвестный тип преобразования: {0}", type));
    }

    /// <summary>
    /// The convert to value.
    /// </summary>
    /// <param name="o">
    /// The o.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T ConvertToValue<T>(object o)
    {
      return (T)ConvertToValue(typeof(T), o);
    }

    /// <summary>
    /// The convert to value.
    /// </summary>
    /// <param name="type">
    /// The type.
    /// </param>
    /// <param name="o">
    /// The o.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public static object ConvertToValue(Type type, object o)
    {
      if ((o != null) && (o.GetType() == type))
      {
        return o;
      }

      if (typeof(Enum).IsAssignableFrom(type))
      {
        var str = o as string;
        if (str != null)
        {
          return Enum.Parse(type, str);
        }

        return Enum.ToObject(type, o);
      }

      var underlyingType = Nullable.GetUnderlyingType(type);
      if (underlyingType != null)
      {
        if (o == null)
        {
          return o;
        }

        type = underlyingType;
      }

      return Convert.ChangeType(o, type);
    }

    /// <summary>
    /// The date time as user string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="localTime">
    /// The local time.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DateTimeAsUserString(DateTime value, bool localTime = true)
    {
      if (localTime)
      {
        value = value.ToLocalTime();
      }

      return value.ToString(DateTimeFormat);
    }

    /// <summary>
    /// The date time as user string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="localTime">
    /// The local time.
    /// </param>
    /// <param name="nullText">
    /// The null text.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DateTimeAsUserString(DateTime? value, bool localTime = true, string nullText = "неизвестно")
    {
      if (value.HasValue)
      {
        return DateTimeAsUserString(value.Value, localTime);
      }

      return nullText;
    }

    /// <summary>
    /// The date time to string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DateTimeToString(DateTime value, string format)
    {
      return DateTime.SpecifyKind(value, DateTimeKind.Unspecified).ToString(format);
    }

    /// <summary>
    /// The date time to string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="format">
    /// The format.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DateTimeToString(DateTime? value, string format)
    {
      if (value.HasValue)
      {
        return DateTimeToString(value.Value, format);
      }

      return string.Empty;
    }

    /// <summary>
    /// The double as user string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DoubleAsUserString(double value)
    {
      return value.ToString((value < 0.001) ? "E3" : "F3");
    }

    /// <summary>
    /// The double to time span.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    public static TimeSpan DoubleToTimeSpan(double value, TimePart timePart)
    {
      switch (timePart)
      {
        case TimePart.Milliseconds:
          return TimeSpan.FromMilliseconds(value);

        case TimePart.Seconds:
          return TimeSpan.FromSeconds(value);

        case TimePart.Minutes:
          return TimeSpan.FromMinutes(value);

        case TimePart.Hours:
          return TimeSpan.FromHours(value);

        case TimePart.Days:
          return TimeSpan.FromDays(value);
      }

      ThrowTimeSpanConversionError(timePart);
      return TimeSpan.Zero;
    }

    /// <summary>
    /// The multiply.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="multiplier">
    /// The multiplier.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    public static TimeSpan Multiply(this TimeSpan value, double multiplier)
    {
      return TimeSpan.FromTicks((long)(value.Ticks * multiplier));
    }

    /// <summary>
    /// The multiply.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="multiplier">
    /// The multiplier.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    public static TimeSpan Multiply(this TimeSpan value, long multiplier)
    {
      return TimeSpan.FromTicks(value.Ticks * multiplier);
    }

    /// <summary>
    /// The search quote.
    /// </summary>
    /// <param name="str">
    /// The str.
    /// </param>
    /// <param name="quote">
    /// The quote.
    /// </param>
    /// <param name="startIndex">
    /// The start index.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// </exception>
    public static int SearchQuote(this string str, ref QuotationMark quote, int startIndex = 0)
    {
      int index;
      if ((str == null) || (startIndex < 0))
      {
        throw new ArgumentException("не могу искать кавычки с заданными параметрами поиска");
      }

      switch (quote)
      {
        case QuotationMark.Apostrophe:
          index = str.IndexOf('\'', startIndex);
          break;

        case QuotationMark.DoubleQuotes:
          index = str.IndexOf('"', startIndex);
          break;

        default:
          index = str.IndexOfAny(quoutes, startIndex);
          break;
      }

      if (index >= 0)
      {
        switch (str[index])
        {
          case '"':
            quote = QuotationMark.DoubleQuotes;
            return index;

          case '\'':
            quote = QuotationMark.Apostrophe;
            break;
        }

        return index;
      }

      quote = QuotationMark.None;
      return index;
    }

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
      string str;
      if ((strValue != null) && ((str = strValue.ToUpper()) != null))
      {
        if (((str == "1") || (str == "TRUE")) || (str == "ДА"))
        {
          return BooleanFlag.True;
        }

        if (((str == "FALSE") || (str == "НЕТ")) || (str == "0"))
        {
          return BooleanFlag.False;
        }
      }

      return BooleanFlag.Unknown;
    }

    /// <summary>
    /// The string to time span.
    /// </summary>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <returns>
    /// The <see cref="TimeSpan"/>.
    /// </returns>
    public static TimeSpan StringToTimeSpan(string strValue, TimePart timePart)
    {
      return DoubleToTimeSpan(StringToValue<double>(strValue), timePart);
    }

    /// <summary>
    /// The string to value.
    /// </summary>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T StringToValue<T>(string strValue)
    {
      return ConvertToValue<T>(strValue);
    }

    /// <summary>
    /// The time ratio as user string.
    /// </summary>
    /// <param name="interval">
    /// The interval.
    /// </param>
    /// <param name="units">
    /// The units.
    /// </param>
    /// <param name="unitName">
    /// The unit name.
    /// </param>
    /// <param name="undefinedText">
    /// The undefined text.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string TimeRatioAsUserString(
      TimeSpan interval, 
      double units, 
      string unitName, 
      string undefinedText = "неизвестно")
    {
      TimePart part;
      string str;
      var d = DoOptimizeTimeRatio(interval, units, out part);
      if (!double.IsNaN(d) && !double.IsInfinity(d))
      {
        switch (part)
        {
          case TimePart.Milliseconds:
            str = "мсек";
            goto Label_0066;

          case TimePart.Seconds:
            str = "сек";
            goto Label_0066;

          case TimePart.Minutes:
            str = "мин";
            goto Label_0066;

          case TimePart.Hours:
            str = "час";
            goto Label_0066;

          case TimePart.Days:
            str = "сутки";
            goto Label_0066;
        }
      }

      return undefinedText;
      Label_0066:
      return string.Format("{0} {1}/{2}", DoubleAsUserString(d), unitName, str);
    }

    /// <summary>
    /// The time span as user string.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string TimeSpanAsUserString(TimeSpan t)
    {
      if (t.TotalMilliseconds <= 1.0)
      {
        return "1 мсек";
      }

      if (t.TotalMilliseconds < 1000.0)
      {
        return string.Format("{0} мсек", Math.Round(t.TotalMilliseconds));
      }

      if (t.TotalSeconds < 60.0)
      {
        return string.Format("{0} сек", t.TotalSeconds);
      }

      var builder = new StringBuilder();
      var totalDays = t.TotalDays;
      if (totalDays >= 1.0)
      {
        var num2 = Math.Floor(totalDays);
        builder.AppendFormat("{0} суток, ", (int)num2);
        totalDays = 24.0 * (totalDays - num2);
      }
      else
      {
        totalDays *= 24.0;
      }

      if (totalDays >= 1.0)
      {
        var num3 = Math.Floor(totalDays);
        builder.AppendFormat("{0:00}:", (int)num3);
        totalDays = 60.0 * (totalDays - num3);
      }
      else
      {
        builder.Append("00:");
        totalDays *= 60.0;
      }

      if (totalDays >= 1.0)
      {
        var num4 = Math.Floor(totalDays);
        builder.AppendFormat("{0:00}:", (int)num4);
        totalDays = 60.0 * (totalDays - num4);
      }
      else
      {
        totalDays *= 60.0;
      }

      builder.AppendFormat("{0:00}", (int)Math.Floor(totalDays));
      return builder.ToString();
    }

    /// <summary>
    /// The time span as user string.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <param name="nullText">
    /// The null text.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string TimeSpanAsUserString(TimeSpan? t, string nullText = "неизвестно")
    {
      if (t.HasValue)
      {
        return TimeSpanAsUserString(t.Value);
      }

      return nullText;
    }

    /// <summary>
    /// The time span to double.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    public static double TimeSpanToDouble(TimeSpan value, TimePart timePart)
    {
      switch (timePart)
      {
        case TimePart.Milliseconds:
          return value.TotalMilliseconds;

        case TimePart.Seconds:
          return value.TotalSeconds;

        case TimePart.Minutes:
          return value.TotalMinutes;

        case TimePart.Hours:
          return value.TotalHours;

        case TimePart.Days:
          return value.TotalDays;
      }

      ThrowTimeSpanConversionError(timePart);
      return double.NaN;
    }

    /// <summary>
    /// The time span to string.
    /// </summary>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string TimeSpanToString(TimeSpan value, TimePart timePart)
    {
      return TimeSpanToDouble(value, timePart).ToString();
    }

    /// <summary>
    /// The try string to version.
    /// </summary>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <returns>
    /// The <see cref="Version"/>.
    /// </returns>
    public static Version TryStringToVersion(string strValue)
    {
      if (!string.IsNullOrEmpty(strValue))
      {
        var strArray = strValue.Split(new[] { '.' });
        var length = strArray.Length;
        if (length <= 4)
        {
          var numArray = new int[4];
          var index = 0;
          while (index < length)
          {
            if (!int.TryParse(strArray[index], out numArray[index]))
            {
              return null;
            }

            index++;
          }

          while (index < 4)
          {
            numArray[index++] = 0;
          }

          return new Version(numArray[0], numArray[1], numArray[2], numArray[3]);
        }
      }

      return null;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The do optimize time ratio.
    /// </summary>
    /// <param name="interval">
    /// The interval.
    /// </param>
    /// <param name="units">
    /// The units.
    /// </param>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <returns>
    /// The <see cref="double"/>.
    /// </returns>
    private static double DoOptimizeTimeRatio(TimeSpan interval, double units, out TimePart timePart)
    {
      if ((interval.TotalMilliseconds >= 1.0) && (units > 0.0))
      {
        while (units < 1.0)
        {
          var totalDays = interval.TotalDays;
          if (totalDays >= 1.0)
          {
            timePart = TimePart.Days;
            return units / totalDays;
          }

          interval = interval.Multiply(10L);
          units *= 10.0;
        }

        timePart = TimePart.Milliseconds;
        var num2 = units / interval.TotalMilliseconds;
        while (num2 < 1.0)
        {
          switch (timePart)
          {
            case TimePart.Milliseconds:
              num2 *= 1000.0;
              timePart = TimePart.Seconds;
              break;

            case TimePart.Seconds:
              num2 *= 60.0;
              timePart = TimePart.Minutes;
              break;

            case TimePart.Minutes:
              num2 *= 60.0;
              timePart = TimePart.Hours;
              break;

            case TimePart.Hours:
              num2 *= 24.0;
              timePart = TimePart.Days;
              break;

            case TimePart.Days:
              return num2;
          }
        }

        return num2;
      }

      timePart = TimePart.Undefined;
      return double.NaN;
    }

    /// <summary>
    /// The do parse date time.
    /// </summary>
    /// <param name="strValue">
    /// The str value.
    /// </param>
    /// <param name="formats">
    /// The formats.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    private static DateTime DoParseDateTime(string strValue, IList<string> formats)
    {
      if ((formats != null) && (formats.Count > 0))
      {
        foreach (var str in formats)
        {
          DateTime time;
          if (DateTime.TryParseExact(
                                     strValue, 
                                     str, 
                                     null, 
                                     DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, 
                                     out time))
          {
            return time;
          }
        }
      }

      return DateTime.Parse(strValue, null, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    }

    /// <summary>
    /// The throw time span conversion error.
    /// </summary>
    /// <param name="timePart">
    /// The time part.
    /// </param>
    /// <exception cref="InvalidCastException">
    /// </exception>
    private static void ThrowTimeSpanConversionError(TimePart timePart)
    {
      throw new InvalidCastException(string.Format("Неизвестный тип преобразования: {0}", timePart));
    }

    #endregion
  }
}