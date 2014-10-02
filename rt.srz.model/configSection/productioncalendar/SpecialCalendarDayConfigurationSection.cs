using System.Configuration;

namespace rt.srz.model.configSection.productioncalendar
{
  public class SpecialCalendarDayConfigurationSection : ConfigurationSection
  {
    [ConfigurationProperty("specialCalendarDays")]
    public SpecialCalendarDayConfigurationCollection SpecialCalendarDays
    {
      get
      {
        return this["specialCalendarDays"] as SpecialCalendarDayConfigurationCollection;
      }
    }
  }
}
