// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRangeNumberManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface RangeNumberManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface RangeNumberManager.
  /// </summary>
  public partial interface IRangeNumberManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="range">
    /// </param>
    void AddOrUpdateRangeNumber(RangeNumber range);

    /// <summary>
    /// ���������� ��� ������
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<RangeNumber> GetRangeNumbers();

    /// <summary>
    /// �������� ������ ��� ������ �� �� �� ������ ���������� ������������� ���������
    /// </summary>
    /// <param name="statement">
    /// </param>
    /// <returns>
    /// The <see cref="Template"/>.
    /// </returns>
    Template GetTemplateByStatement(Statement statement);

    /// <summary>
    /// ������������ �� ��������� ������ � ������� �� ���������. ������ ��� ���������� � ������ �� = null,
    ///   �.�. ��� �������� ����������� ������� ���������� �� ����� ��������
    /// </summary>
    /// <param name="range">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IntersectWithOther(RangeNumber range);

    #endregion
  }
}