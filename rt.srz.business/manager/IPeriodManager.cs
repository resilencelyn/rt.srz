//-------------------------------------------------------------------------------------
// <copyright file="IPeriodManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System.Collections.Generic;
using System.ServiceModel;
namespace rt.srz.business.manager
{
  using System;

  using rt.srz.model.srz;

  /// <summary>
  /// The interface PeriodManager.
  /// </summary>
  public partial interface IPeriodManager
  {
    /// <summary>
    /// The get period by month.
    /// </summary>
    /// <param name="date">
    /// The date.
    /// </param>
    /// <returns>
    /// The <see cref="Period"/>.
    /// </returns>
    Period GetPeriodByMonth(DateTime date);
    
    /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго отправителя либо получателя
    /// </summary>
    /// <returns></returns>
    IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId);
  }
}