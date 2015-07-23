// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BirthDayType.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The birthday type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.enumerations
{
  /// <summary>
  ///   The birthday type.
  /// </summary>
  public enum BirthdayType
  {
    /// <summary> The unknown. </summary>
    Unknown = 0, // Неизвестный тип

    /// <summary>
    ///   The full.
    /// </summary>
    Full = 1, // Известна полностью

    /// <summary>
    ///   The month and year.
    /// </summary>
    MonthAndYear = 2, // Известен месяц и год

    /// <summary>
    ///   The year.
    /// </summary>
    Year = 3 // Известен год
  }
}