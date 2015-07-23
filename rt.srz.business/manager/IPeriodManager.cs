// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPeriodManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface PeriodManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface PeriodManager.
  /// </summary>
  public partial interface IPeriodManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает сортированный список периодов в которых запускались пакетные операции экспорта в СМО для указаннго
    ///   отправителя либо получателя
    /// </summary>
    /// <param name="senderId">
    /// The sender Id.
    /// </param>
    /// <param name="receiverId">
    /// The receiver Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<Period> GetExportSmoBatchPeriodList(Guid senderId, Guid receiverId);

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

    #endregion
  }
}