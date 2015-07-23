// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversionHelper.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The conversion helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard.keyscompiler.Rules
{
  using System;

  /// <summary>
  /// The conversion helper.
  /// </summary>
  public static class ConversionHelper
  {
    // формат вывода даты/времени
    #region Static Fields

    /// <summary>
    /// The date time format.
    /// </summary>
    public static readonly string DateTimeFormat = @"yyyy'/'MM'/'dd' 'HH':'mm':'ss'.'fff";

    // нулевые дата/время
    /// <summary>
    /// The date time zero.
    /// </summary>
    public static readonly DateTime DateTimeZero = new DateTime();

    #endregion

    // --------------------------------------------------------
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
  }
}