// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BirthDayType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
    Unknown = 0, // ����������� ���
    /// <summary>
    ///   The full.
    /// </summary>
    Full = 1, // �������� ���������
    /// <summary>
    ///   The month and year.
    /// </summary>
    MonthAndYear = 2, // �������� ����� � ���
    /// <summary>
    ///   The year.
    /// </summary>
    Year = 3 // �������� ���
  }
}