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
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="range"></param>
    public void AddOrUpdateRangeNumber(RangeNumber range)
    {
      //�������������� ������ ������������� ���� ��� � ���� � ���� ��� � �������
      //������� �� ���� �� �������� ��� ������� ���� ������� � range.RangeNumbers

      var rangeInDatabase = GetById(range.Id);
      var listToDelete = rangeInDatabase.RangeNumbers.Where(x => !range.RangeNumbers.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Evict(rangeInDatabase);

      foreach (var item in listToDelete)
      {
        Delete(item);
      }

      //��������� ������ �� �������
      foreach (var item in range.RangeNumbers)
      {
        //���� ������� ���� � ���� �������� �� - ������ ��� �����
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
    /// ������������ �� ��������� ������ � ������� �� ���������. ������ ��� ���������� � ������ �� = null, 
    /// �.�. ��� �������� ����������� ������� ���������� �� ����� ��������
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
    /// �������� ������ ��� ������ �� �� �� ������ ���������� ������������� ���������
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    public Template GetTemplateVsByStatement(Statement statement)
    {
      int num = int.Parse(statement.NumberTemporaryCertificate);

      // ����� ������� ��������
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

      //���� ����� ���� ������������� �� ����� ������� (�������� ��� ������ �� �������), �� ���������� ������ ������ �� ���������
      var defaultTemplate = ObjectFactory.GetInstance<ITemplateManager>().GetBy(x => x.Default == true).SingleOrDefault();
      return defaultTemplate;
    }

    /// <summary>
    /// ���������� ��� ������
    /// </summary>
    /// <returns></returns>
    public IList<RangeNumber> GetRangeNumbers()
    {
      var sec = ObjectFactory.GetInstance<ISecurityProvider>();
      //��� �������� ������������
      var smoId = sec.GetCurrentUser().PointDistributionPolicy.Parent.Id;
      //���������� ������ ��������� ����� ��� � �������� ������ � ������ ���������
      return GetAll(int.MaxValue).Where(x => x.Smo.Id == smoId && x.Parent == null).ToList();
    }

  }
}