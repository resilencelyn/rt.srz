// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Age.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  using System;

  /// <summary>
  ///   The age.
  /// </summary>
  public static class Age
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate age.
    /// </summary>
    /// <param name="birthDate">
    /// The birth date.
    /// </param>
    /// <returns>
    /// The <see cref="int"/> .
    /// </returns>
    public static int CalculateAge(DateTime birthDate)
    {
      return CalculateAgeOnDate(birthDate, DateTime.Now);
    }

    /// <summary>
    /// The calculate age on date.
    /// </summary>
    /// <param name="birthDate">
    /// The birth date.
    /// </param>
    /// <param name="onDate">
    /// The on date.
    /// </param>
    /// <returns>
    /// The <see cref="int"/> .
    /// </returns>
    public static int CalculateAgeOnDate(DateTime birthDate, DateTime onDate)
    {
      var yearsPassed = onDate.Year - birthDate.Year;
      if (onDate.Month < birthDate.Month || (onDate.Month == birthDate.Month && onDate.Day < birthDate.Day))
      {
        yearsPassed--;
      }

      return yearsPassed;
    }

    #endregion
  }
}