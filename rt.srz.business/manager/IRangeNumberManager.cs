// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRangeNumberManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface RangeNumberManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface RangeNumberManager.
  /// </summary>
  public partial interface IRangeNumberManager
  {
    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="range"></param>
    void AddOrUpdateRangeNumber(RangeNumber range);

    /// <summary>
    /// ������������ �� ��������� ������ � ������� �� ���������. ������ ��� ���������� � ������ �� = null, 
    /// �.�. ��� �������� ����������� ������� ���������� �� ����� ��������
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    bool IntersectWithOther(RangeNumber range);

    /// <summary>
    /// �������� ������ ��� ������ �� �� �� ������ ���������� ������������� ���������
    /// </summary>
    /// <param name="statement"></param>
    /// <returns></returns>
    Template GetTemplateVsByStatement(Statement statement);

    /// <summary>
    /// ���������� ��� ������
    /// </summary>
    /// <returns></returns>
    IList<RangeNumber> GetRangeNumbers();


  }
}