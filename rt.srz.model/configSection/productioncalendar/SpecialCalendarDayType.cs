// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialCalendarDayType.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The special calendar day type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.configSection.productioncalendar
{
  /// <summary>
  ///   The special calendar day type.
  /// </summary>
  public enum SpecialCalendarDayType
  {
    /// <summary>
    ///   Выходной день, выпадающий на рабочий.
    /// </summary>
    FreeWorkingDay, 

    /// <summary>
    ///   Рабочий день, выпадающий на выходной.
    /// </summary>
    WorkingHolidayDay
  }
}