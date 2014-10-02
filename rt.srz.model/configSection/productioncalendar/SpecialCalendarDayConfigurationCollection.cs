using System.Configuration;

namespace rt.srz.model.configSection.productioncalendar
{
  public class SpecialCalendarDayConfigurationCollection : ConfigurationElementCollection
  {
    public SpecialCalendarDayConfigurationElement this[int index]
    {
      get
      {
        return base.BaseGet(index) as SpecialCalendarDayConfigurationElement;
      }
      set
      {
        if (base.BaseGet(index) != null)
        {
          base.BaseRemoveAt(index);
        }
        this.BaseAdd(index, value);
      }
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new SpecialCalendarDayConfigurationElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((SpecialCalendarDayConfigurationElement)element).Date;
    } 
  }
}
