// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IpersonManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface personManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.atl.model.dto;
using System.Collections.Generic;
namespace rt.atl.business.manager
{
  /// <summary>
  ///   The interface personManager.
  /// </summary>
  public partial interface IpersonManager
  {
    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    IList<StatisticInitialLoading> GetStatisticInitialLoading();
  }
}