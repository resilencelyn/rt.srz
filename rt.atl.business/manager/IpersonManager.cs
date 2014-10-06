// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IpersonManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface personManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.manager
{
  using System.Collections.Generic;

  using rt.atl.model.dto;

  /// <summary>
  ///   The interface personManager.
  /// </summary>
  public partial interface IpersonManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ��������� ��������
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<StatisticInitialLoading> GetStatisticInitialLoading();

    #endregion
  }
}