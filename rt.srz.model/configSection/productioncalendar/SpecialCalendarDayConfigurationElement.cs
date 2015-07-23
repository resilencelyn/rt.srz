// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialCalendarDayConfigurationElement.cs" company="јль€нс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ќписывает либо выходной день, выпадающий на рабочий
//   либо рабочий день, выпадающий на выходной
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.configSection.productioncalendar
{
  using System;
  using System.Configuration;

  /// <summary>
  ///   ќписывает либо выходной день, выпадающий на рабочий
  ///   либо рабочий день, выпадающий на выходной
  /// </summary>
  public class SpecialCalendarDayConfigurationElement : ConfigurationElement
  {
    #region Public Properties

    /// <summary>
    ///   ƒата
    /// </summary>
    [ConfigurationProperty("Date", IsRequired = true)]
    public DateTime Date
    {
      get
      {
        return (DateTime)this["Date"];
      }
    }

    /// <summary>
    ///   “ип
    /// </summary>
    [ConfigurationProperty("Type", IsRequired = true)]
    public SpecialCalendarDayType Type
    {
      get
      {
        return (SpecialCalendarDayType)Enum.Parse(typeof(SpecialCalendarDayType), this["Type"].ToString());
      }
    }

    #endregion
  }
}