// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The srz config manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration
{
  #region references

  using System.Configuration;

  using rt.srz.model.configSection.productioncalendar;

  #endregion

  /// <summary>
  ///   The srz config manager.
  /// </summary>
  public static class ConfigManager
  {
    #region Static Fields

    /// <summary>
    ///   The calendar day configuration.
    /// </summary>
    private static SpecialCalendarDayConfigurationSection calendarDayConfiguration;

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the calendar day configuration.
    /// </summary>
    public static SpecialCalendarDayConfigurationSection CalendarDayConfiguration
    {
      get
      {
        return calendarDayConfiguration
               ?? (calendarDayConfiguration =
                   (SpecialCalendarDayConfigurationSection)ConfigurationManager.GetSection("SpecialCalendarDaysSection"));
      }
    }

    #endregion
  }
}