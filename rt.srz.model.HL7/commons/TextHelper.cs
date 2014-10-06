// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The text helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons
{
  #region references

  using System;
  using System.Text;

  using rt.srz.model.HL7.dotNetX;

  #endregion

  /// <summary>
  ///   The text helper.
  /// </summary>
  public static class TextHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The discard format.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string DiscardFormat(this string s)
    {
      if (s == null)
      {
        return string.Empty;
      }

      return s.Replace("{", "{{").Replace("}", "}}");
    }

    /// <summary>
    /// The discard format.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="StringBuilder"/>.
    /// </returns>
    public static StringBuilder DiscardFormat(this StringBuilder s)
    {
      if (s == null)
      {
        return new StringBuilder();
      }

      return s.Replace("{", "{{").Replace("}", "}}");
    }

    /// <summary>
    /// The plain whitespaces.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string PlainWhitespaces(this string s)
    {
      return TStringHelper.ReplaceAny(s, ' ', new[] { '\t', '\r', '\n' });
    }

    /// <summary>
    /// The retrieve compare options.
    /// </summary>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <returns>
    /// The <see cref="StringComparison"/>.
    /// </returns>
    public static StringComparison RetrieveCompareOptions(bool ignoreCase = false)
    {
      if (!ignoreCase)
      {
        return StringComparison.Ordinal;
      }

      return StringComparison.OrdinalIgnoreCase;
    }

    /// <summary>
    ///   The retrieve compare options ic.
    /// </summary>
    /// <returns>
    ///   The <see cref="StringComparison" />.
    /// </returns>
    public static StringComparison RetrieveCompareOptionsIC()
    {
      var ignoreCase = true;
      return RetrieveCompareOptions(ignoreCase);
    }

    /// <summary>
    /// The retrieve comparer.
    /// </summary>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <returns>
    /// The <see cref="StringComparer"/>.
    /// </returns>
    public static StringComparer RetrieveComparer(bool ignoreCase = false)
    {
      if (!ignoreCase)
      {
        return StringComparer.Ordinal;
      }

      return StringComparer.OrdinalIgnoreCase;
    }

    /// <summary>
    ///   The retrieve comparer ic.
    /// </summary>
    /// <returns>
    ///   The <see cref="StringComparer" />.
    /// </returns>
    public static StringComparer RetrieveComparerIC()
    {
      var ignoreCase = true;
      return RetrieveComparer(ignoreCase);
    }

    /// <summary>
    /// The starts with.
    /// </summary>
    /// <param name="s">
    /// The s.
    /// </param>
    /// <param name="value">
    /// The value.
    /// </param>
    /// <param name="ignoreCase">
    /// The ignore case.
    /// </param>
    /// <param name="separately">
    /// The separately.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool StartsWith(this string s, string value, bool ignoreCase = false, bool separately = false)
    {
      if ((string.IsNullOrEmpty(s) || string.IsNullOrEmpty(value))
          || !s.StartsWith(value, RetrieveCompareOptions(ignoreCase)))
      {
        return false;
      }

      if ((separately && (s.Length > value.Length)) && char.IsLetterOrDigit(s, value.Length))
      {
        return false;
      }

      return true;
    }

    #endregion
  }
}