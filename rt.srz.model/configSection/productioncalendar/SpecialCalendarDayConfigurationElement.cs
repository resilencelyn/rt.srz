using System;
using System.Configuration;

namespace rt.srz.model.configSection.productioncalendar
{
  /// <summary>
  /// ��������� ���� �������� ����, ���������� �� �������
  /// ���� ������� ����, ���������� �� ��������
  /// </summary>
  public class SpecialCalendarDayConfigurationElement : ConfigurationElement
  {
    /// <summary>
    /// ����
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
    /// ���
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
