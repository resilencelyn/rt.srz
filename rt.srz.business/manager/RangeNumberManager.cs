// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RangeNumberManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using rt.core.business.security.interfaces;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The RangeNumberManager.
  /// </summary>
  public partial class RangeNumberManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="range">
    /// </param>
    public void AddOrUpdateRangeNumber(RangeNumber range)
    {
      // �������������� ������ ������������� ���� ��� � ���� � ���� ��� � �������
      // ������� �� ���� �� �������� ��� ������� ���� ������� � range.RangeNumbers
      var rangeInDatabase = GetById(range.Id);
      var listToDelete =
        rangeInDatabase.RangeNumbers.Where(x => !range.RangeNumbers.Select(y => y.Id).ToList().Contains(x.Id)).ToList();

      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Evict(rangeInDatabase);

      foreach (var item in listToDelete)
      {
        Delete(item);
      }

      // ��������� ������ �� �������
      foreach (var item in range.RangeNumbers)
      {
        // ���� ������� ���� � ���� �������� �� - ������ ��� �����
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
    /// ���������� ��� ������
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<RangeNumber> GetRangeNumbers()
    {
      var user = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();

      // ��� �������� ������������
      var smoId = user.GetSmo().Id;

      // ���������� ������ ��������� ����� ��� � �������� ������ � ������ ���������
      return GetAll(int.MaxValue).Where(x => x.Smo.Id == smoId && x.Parent == null).ToList();
    }

    /// <summary>
    /// �������� ������ ��� ������ �� �� �� ������ ���������� ������������� ���������
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    public Template GetTemplateVsByStatement(Statement statement)
    {
      var num = int.Parse(statement.NumberTemporaryCertificate);

      // ����� ������� ��������
      var mainRange =
        GetBy(
              x =>
              x.Smo == statement.PointDistributionPolicy.Parent && x.RangelFrom <= num && num <= x.RangelTo
              && x.Parent == null).FirstOrDefault();
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

      // ���� ����� ���� ������������� �� ����� ������� (�������� ��� ������ �� �������), �� ���������� ������ ������ �� ���������
      var defaultTemplate =
        ObjectFactory.GetInstance<ITemplateManager>().GetBy(x => x.Default == true).SingleOrDefault();
      return defaultTemplate;
    }

    /// <summary>
    /// ������������ �� ��������� ������ � ������� �� ���������. ������ ��� ���������� � ������ �� = null,
    ///   �.�. ��� �������� ����������� ������� ���������� �� ����� ��������
    /// </summary>
    /// <param name="range">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IntersectWithOther(RangeNumber range)
    {
      var intervals = GetBy(x => x.Smo == range.Smo && x.Id != range.Id && x.Parent == null);
      intervals.Add(range);
      intervals.OrderBy(x => x.RangelFrom);

      var started = new RangeNumber();
      for (var i = 0; i < intervals.Count; i++)
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

    #endregion
  }
}