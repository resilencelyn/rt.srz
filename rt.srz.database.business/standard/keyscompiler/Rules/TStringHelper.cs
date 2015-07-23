// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TStringHelper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The t string helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;
  using System.Text;

  /// <summary>
  /// The t string helper.
  /// </summary>
  public static class TStringHelper
  {
    // ---------------------------------------------------------------------------------------------------------------

    // IsNullOrEmpty: универсальная перегрузка (для str, StringBuilder...)
    #region Public Methods and Operators

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
            {
              space = i;
            }
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
              {
                result = new StringBuilder();
              }

              result.Append(str.Substring(start, space - start));
              if (Space != '\0')
              {
                result.Append(Space);
              }

              start = i;
              space = -1;
            }
          }
        }

        // если что-то заменяли, возвращаем полученную строку
        if (result != null)
        {
          if (start < len)
          {
            result.Append(str.Substring(start));
          }

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

    // ---------------------------------------------------------------------------------------------------------------

    // привести строку к заданному виду, если она ПУСТАЯ
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
    /// <summary>
    /// The string shrink.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="emptyType">
    /// The empty type.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringShrink(string str, TEmptyStringTypes emptyType, bool bTrimFirst)
    {
      if (bTrimFirst && str != null)
      {
        str = str.Trim();
      }

      if (string.IsNullOrEmpty(str))
      {
        switch (emptyType)
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
    /// The string to empty.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToEmpty(string str, bool bTrimFirst)
    {
      return StringShrink(str, TEmptyStringTypes.Empty, bTrimFirst);
    }

    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <param name="bTrimFirst">
    /// The b trim first.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(string str, bool bTrimFirst)
    {
      return StringShrink(str, TEmptyStringTypes.Null, bTrimFirst);
    }

    // упрощенная версия StringToNull()
    // !! bTrimFirst == false
    /// <summary>
    /// The string to null.
    /// </summary>
    /// <param name="str">
    /// The string.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string StringToNull(string str)
    {
      return StringToNull(str, /*bTrimFirst*/ false);
    }

    #endregion

    // возвратить string.Empty, если на входе пустая строка (в т.ч. null)
    // !! непустые строки возвращаются как-есть, с наложением условия bTrimFirst
  }
}