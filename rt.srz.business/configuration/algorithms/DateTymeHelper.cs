// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTymeHelper.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The date tyme helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.configuration.algorithms
{
  #region references

  using System;
  using System.Linq;

  using StructureMap;

  using rt.atl.model.atl;
  using rt.core.business.nhibernate;
  using rt.srz.model.configSection.productioncalendar;

  #endregion

  /// <summary>
  ///   The date tyme helper.
  /// </summary>
  public static class DateTymeHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The calculate en period working day.
    /// </summary>
    /// <param name="dateFrom">
    /// The date from.
    /// </param>
    /// <param name="count">
    /// The count.
    /// </param>
    /// <returns>
    /// The <see cref="DateTime"/>.
    /// </returns>
    public static DateTime CalculateEnPeriodWorkingDay(DateTime dateFrom, int count)
    {
      try
      {
        var sessionFactorySrz = ObjectFactory.GetInstance<IManagerSessionFactorys>().GetFactoryByName("NHibernateCfgAtl.xml");

        using (var session = sessionFactorySrz.OpenSession())
        {
          var vsend = session.QueryOver<Vsend>().Where(x => x.Db == dateFrom).List().FirstOrDefault();
          if (vsend != null && vsend.De.HasValue)
          {
            return vsend.De.Value;
          }
        }
      }
      catch
      {
      }

      var specialCalendarDays =
        ConfigManager.CalendarDayConfiguration.SpecialCalendarDays.Cast<SpecialCalendarDayConfigurationElement>()
          .ToList();
      var date = dateFrom;
      for (var i = 0; i < count; date = date.AddDays(1))
      {
        // Выходной праздничный день 
        var f1 = specialCalendarDays.Any(x => x.Date == date && x.Type == SpecialCalendarDayType.FreeWorkingDay);

        // Не рабочий день в суботу и вск.
        var f2 = !specialCalendarDays.Any(x => x.Date == date && x.Type == SpecialCalendarDayType.WorkingHolidayDay);

        // Определяем выходной
        if ((date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || f1) && f2)
        {
          continue;
        }

        i++;
      }

      return date.AddDays(-1);
    }

    #endregion
  }
}