// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeNumberManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The RangeNumberManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.core.business.security.interfaces;
using rt.srz.model.srz;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
namespace rt.srz.business.manager
{
  using NHibernate;

  /// <summary>
  ///   The RangeNumberManager.
  /// </summary>
  public partial class RangeNumberManager
  {
    /// <summary>
    /// ƒобавление или обновление записи
    /// </summary>
    /// <param name="range"></param>
    public void AddOrUpdateRangeNumber(RangeNumber range)
    {
      //синхронизируем список поддиапазонов того что в базе и того что в объекте
      //удал€ем из базы те элементы дл€ которых нету записей в range.RangeNumbers

      var rangeInDatabase = GetById(range.Id);
      var listToDelete = rangeInDatabase.RangeNumbers.Where(x => !range.RangeNumbers.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Evict(rangeInDatabase);

      foreach (var item in listToDelete)
      {
        Delete(item);
      }

      //сохран€ем список из объекта
      foreach (var item in range.RangeNumbers)
      {
        //если объекта нету в базе обнул€ем ид - значит это новый
        var subrangeInDatabase = GetById(item.Id);
        if (subrangeInDatabase == null)
        {
          item.Id = Guid.Empty;
        }
        ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Evict(subrangeInDatabase);
        SaveOrUpdate(item);
      }
      SaveOrUpdate(range);
    }

    /// <summary>
    /// ѕересекаетс€ ли указанна€ запись с другими по диапозону. “олько дл€ диапазонов с парент ид = null, 
    /// т.е. это проверка пересечений главных диапазонов из шапки страницы
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IntersectWithOther(RangeNumber range)
    {
      var intervals = GetBy(x => x.Smo == range.Smo && x.Id != range.Id && x.Parent == null);
      intervals.Add(range);
      intervals.OrderBy(x => x.RangelFrom);

      var started = new RangeNumber();
      for (int i = 0; i < intervals.Count; i++)
      {
        if (i == 0)
        {
          started = intervals[0];
        }
        else
        {
          if (started.RangelFrom <= intervals[i].RangelFrom && intervals[i].RangelFrom <= started.RangelTo)
          {
            return true;
          }
          started = intervals[i];
        }
      }
      return false;
    }

    /// <summary>
    /// ѕолучает шаблон дл€ печати вс по по номеру временного свидетельства за€влени€
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public Template GetTemplateVsByStatement(Statement statement)
    {
      int num = int.Parse(statement.NumberTemporaryCertificate);

      // нашли главный интервал
      RangeNumber mainRange = GetBy(x => x.Smo == statement.PointDistributionPolicy.Parent &&
        x.RangelFrom <= num && num <= x.RangelTo && x.Parent == null).FirstOrDefault();
      if (mainRange != null)
      {
        foreach (var subRange in mainRange.RangeNumbers)
        {
          if (subRange.RangelFrom <= num && num <= subRange.RangelTo)
          {
            return subRange.Template;
          }
        }
      }

      //если среди всех поддиапазонов не нашли нужного (например они вообще не указаны), то используем шаблон печати по умолчанию
      var defaultTemplate = ObjectFactory.GetInstance<ITemplateManager>().GetBy(x => x.Default == true).SingleOrDefault();
      return defaultTemplate;
    }

    /// <summary>
    /// «ачитывает все записи
    /// </summary>
    /// <returns></returns>
    public IList<RangeNumber> GetRangeNumbers()
    {
      var sec = ObjectFactory.GetInstance<ISecurityProvider>();
      //смо текущего пользовател€
      var smoId = sec.GetCurrentUser().PointDistributionPolicy.Parent.Id;
      //отображаем только диапазоны своей смо и верхнего уровн€ с пустым родителем
      return GetAll(int.MaxValue).Where(x => x.Smo.Id == smoId && x.Parent == null).ToList();
    }

  }
}