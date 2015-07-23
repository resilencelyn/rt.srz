// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialCalendarDayConfigurationSection.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The special calendar day configuration section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.configSection.productioncalendar
{
  using System.Configuration;

  /// <summary>
  /// The special calendar day configuration section.
  /// </summary>
  public class SpecialCalendarDayConfigurationSection : ConfigurationSection
  {
    #region Public Properties

    /// <summary>
    /// Gets the special calendar days.
    /// </summary>
    [ConfigurationProperty("specialCalendarDays")]
    public SpecialCalendarDayConfigurationCollection SpecialCalendarDays
    {
      get
      {
        return this["specialCalendarDays"] as SpecialCalendarDayConfigurationCollection;
      }
    }

    #endregion
  }
}