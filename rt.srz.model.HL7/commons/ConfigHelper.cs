// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The config helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.Collections.Specialized;
  using System.Configuration;
  using System.Linq;
  using System.Text;
  using System.Text.RegularExpressions;

  using rt.srz.model.HL7.dotNetX;

  #endregion

  /// <summary>
  ///   The config helper.
  /// </summary>
  public static class ConfigHelper
  {
    #region Static Fields

    /// <summary>
    ///   The app settings.
    /// </summary>
    public static NameValueCollection AppSettings = ConfigurationManager.AppSettings;

    /// <summary>
    ///   The connection strings.
    /// </summary>
    public static ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;

    /// <summary>
    ///   The regex environment variable.
    /// </summary>
    private static readonly Regex regexEnvironmentVariable = RegexHelper.TryCreateRegex("%EnvVar:([^%]*)%", false);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckConfigValue(string name)
    {
      return CheckConfigValue<string>(name);
    }

    /// <summary>
    /// The check config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckConfigValue<T>(string name)
    {
      var rawRead = false;
      return DoCheckConfigValue<T>(name, rawRead);
    }

    /// <summary>
    /// The check config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckConfigValue<T>(string name, T min, T max, params T[] allowed) where T : IComparable<T>
    {
      var rawRead = false;
      return DoCheckConfigValue(name, min, max, allowed, rawRead);
    }

    /// <summary>
    /// The check raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckRawConfigValue(string name)
    {
      return CheckRawConfigValue<string>(name);
    }

    /// <summary>
    /// The check raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckRawConfigValue<T>(string name)
    {
      var rawRead = true;
      return DoCheckConfigValue<T>(name, rawRead);
    }

    /// <summary>
    /// The check raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool CheckRawConfigValue<T>(string name, T min, T max, params T[] allowed) where T : IComparable<T>
    {
      var rawRead = true;
      return DoCheckConfigValue(name, min, max, allowed, rawRead);
    }

    /// <summary>
    /// The raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T RawConfigValue<T>(string name, T dflt)
    {
      var rawRead = true;
      return DoReadConfigValue(name, dflt, rawRead);
    }

    /// <summary>
    /// The raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool RawConfigValue<T>(string name, ref T value)
    {
      var rawRead = true;
      return DoReadConfigValue(name, ref value, rawRead);
    }

    /// <summary>
    /// The raw config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T RawConfigValue<T>(string name, T dflt, T min, T max, params T[] allowed) where T : IComparable<T>
    {
      var rawRead = true;
      return DoReadConfigValue(name, dflt, min, max, allowed, rawRead);
    }

    /// <summary>
    /// The read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T ReadConfigValue<T>(string name, T dflt)
    {
      var rawRead = false;
      return DoReadConfigValue(name, dflt, rawRead);
    }

    /// <summary>
    /// The read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool ReadConfigValue<T>(string name, ref T value)
    {
      var rawRead = false;
      return DoReadConfigValue(name, ref value, rawRead);
    }

    /// <summary>
    /// The read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T ReadConfigValue<T>(string name, T dflt, T min, T max, params T[] allowed) where T : IComparable<T>
    {
      var rawRead = false;
      return DoReadConfigValue(name, dflt, min, max, allowed, rawRead);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The do check config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoCheckConfigValue<T>(string name, bool rawRead)
    {
      var local = default(T);
      return DoReadConfigValue(name, ref local, rawRead);
    }

    /// <summary>
    /// The do check config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoCheckConfigValue<T>(string name, T min, T max, T[] allowed, bool rawRead)
      where T : IComparable<T>
    {
      var local = default(T);
      return DoReadConfigValue(name, ref local, min, max, allowed, rawRead);
    }

    /// <summary>
    /// The do read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoReadConfigValue<T>(string name, ref T value, bool rawRead)
    {
      Func<Match, int> func = null;
      try
      {
        var bTrimFirst = true;
        var input = TStringHelper.StringToNull(AppSettings[name], bTrimFirst);
        if (input != null)
        {
          if (!rawRead)
          {
            var matchCollection = regexEnvironmentVariable.Matches(input);
            if ((matchCollection != null) && (matchCollection.Count > 0))
            {
              var builder = new StringBuilder(input);
              if (func == null)
              {
                func = row => row.Index;
              }

              foreach (var match in matchCollection.EnumerateMatches().OrderByDescending(func))
              {
                var str2 = match.Groups[1].Value;
                var str3 = !string.IsNullOrEmpty(str2)
                             ? TStringHelper.StringToEmpty(Environment.GetEnvironmentVariable(str2))
                             : string.Empty;
                builder.Remove(match.Index, match.Length);
                builder.Insert(match.Index, str3);
              }

              input = builder.ToString();
            }
          }

          value = ConversionHelper.StringToValue<T>(input);
          return true;
        }
      }
      catch
      {
      }

      return false;
    }

    /// <summary>
    /// The do read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    private static T DoReadConfigValue<T>(string name, T dflt, bool rawRead)
    {
      DoReadConfigValue(name, ref dflt, rawRead);
      return dflt;
    }

    /// <summary>
    /// The do read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool DoReadConfigValue<T>(string name, ref T value, T min, T max, T[] allowed, bool rawRead)
      where T : IComparable<T>
    {
      try
      {
        if (DoReadConfigValue(name, ref value, rawRead))
        {
          if ((value.CompareTo(min) >= 0) && (value.CompareTo(max) <= 0))
          {
            return true;
          }

          if (allowed != null)
          {
            var length = allowed.Length;
            while (length > 0)
            {
              if (value.CompareTo(allowed[--length]) == 0)
              {
                return true;
              }
            }
          }
        }
      }
      catch
      {
      }

      return false;
    }

    /// <summary>
    /// The do read config value.
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <param name="dflt">
    /// The dflt.
    /// </param>
    /// <param name="min">
    /// The min.
    /// </param>
    /// <param name="max">
    /// The max.
    /// </param>
    /// <param name="allowed">
    /// The allowed.
    /// </param>
    /// <param name="rawRead">
    /// The raw read.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    private static T DoReadConfigValue<T>(string name, T dflt, T min, T max, T[] allowed, bool rawRead)
      where T : IComparable<T>
    {
      var local = dflt;
      if (DoReadConfigValue(name, ref local, min, max, allowed, rawRead))
      {
        return local;
      }

      return dflt;
    }

    #endregion
  }
}