// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialCalendarDayConfigurationElement.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ��������� ���� �������� ����, ���������� �� �������
//   ���� ������� ����, ���������� �� ��������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.configSection.productioncalendar
{
  using System;
  using System.Configuration;

  /// <summary>
  ///   ��������� ���� �������� ����, ���������� �� �������
  ///   ���� ������� ����, ���������� �� ��������
  /// </summary>
  public class SpecialCalendarDayConfigurationElement : ConfigurationElement
  {
    #region Public Properties

    /// <summary>
    ///   ����
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
    ///   ���
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