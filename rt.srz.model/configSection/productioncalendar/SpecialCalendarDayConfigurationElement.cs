using System;
using System.Configuration;

namespace rt.srz.model.configSection.productioncalendar
{
  /// <summary>
  /// Описывает либо выходной день, выпадающий на рабочий
  /// либо рабочий день, выпадающий на выходной
  /// </summary>
  public class SpecialCalendarDayConfigurationElement : ConfigurationElement
  {
    /// <summary>
    /// Дата
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
    /// Тип
    /// </summary>
    [ConfigurationProperty("Type", IsRequired = true)]
    public SpecialCalendarDayType Type
    {
      get
      {
        return (SpecialCalendarDayType)Enum.Parse(typeof(SpecialCalendarDayType), this["Type"].ToString());
      }
    }
  }
}
