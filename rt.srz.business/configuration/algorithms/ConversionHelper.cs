// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversionHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Преобразователь
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration.algorithms
{
  #region references

  using System;
  using System.Globalization;

  #endregion

  /// <summary>
  ///   Преобразователь
  /// </summary>
  public class ConversionHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// Преобразование даты в строку
    /// </summary>
    /// <param name="date">
    /// date
    /// </param>
    /// <returns>
    /// строка в формате для ФОМС
    /// </returns>
    public static string DateTimeToString(DateTime date)
    {
      var strdate = date.ToString("s");
      strdate += "Z";
      if (TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds > 0)
      {
        strdate += TimeZoneInfo.Local.BaseUtcOffset.ToString(@"\+hh\:mm");
      }
      else
      {
        strdate += TimeZoneInfo.Local.BaseUtcOffset.ToString(@"\-hh\:mm");
      }

      return strdate;
    }

    /// <summary>
    /// Введена потому что, для ГОЗНАКА буква Z не нужна перед зоной))))
    /// </summary>
    /// <param name="date">
    /// date
    /// </param>
    /// <returns>
    /// строка в формате для ФОМС
    /// </returns>
    public static string DateTimeToStringGoznak(DateTime date)
    {
      var strdate = date.ToString("s");
      if (TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds > 0)
      {
        strdate += TimeZoneInfo.Local.BaseUtcOffset.ToString(@"\+hh\:mm");
      }
      else
      {
        strdate += TimeZoneInfo.Local.BaseUtcOffset.ToString(@"\-hh\:mm");
      }

      return strdate;
    }

    /// <summary>
    /// Преобразование даты в строку yyyy-MM-dd
    /// </summary>
    /// <param name="date">
    /// date
    /// </param>
    /// <returns>
    /// строка в формате для ФОМС
    /// </returns>
    public static string DateTimeToStringShort(DateTime date)
    {
      return date.GetDefaultForIncorrectDate("yyyy-MM-dd");
    }

    /// <summary>
    /// Преобразование строки в дата
    /// </summary>
    /// <param name="str">
    /// строка в формате для ФОМС
    /// </param>
    /// <param name="dateTime">
    /// значение по умолчанию
    /// </param>
    /// <returns>
    /// дата
    /// </returns>
    public static DateTime TryStringToDateTime(string str, DateTime dateTime)
    {
      DateTime? dt = dateTime;
      var tryStringToDateTime = TryStringToDateTime(str, dt);
      if (tryStringToDateTime != null)
      {
        return tryStringToDateTime.Value;
      }

      return new DateTime(1900, 1, 1);
    }

    /// <summary>
    /// Преобразование строки в дата
    /// </summary>
    /// <param name="str">
    /// строка в формате для ФОМС
    /// </param>
    /// <param name="dateTime">
    /// значение по умолчанию
    /// </param>
    /// <returns>
    /// дата
    /// </returns>
    public static DateTime? TryStringToDateTime(string str, DateTime? dateTime)
    {
      DateTime dt;
      var formats = new[]
                      {
                        "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'zzz", "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz", "yyyy'-'MM'-'dd", 
                        "dd'.'MM'.'yyyy"
                      };
      if (DateTime.TryParseExact(str, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
      {
        return dt;
      }

      return dateTime;
    }

    #endregion
  }
}