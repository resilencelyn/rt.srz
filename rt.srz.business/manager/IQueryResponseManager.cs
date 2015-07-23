// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryResponseManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface QueryResponseManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  ///   The interface QueryResponseManager.
  /// </summary>
  public partial interface IQueryResponseManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Данные для ненайденных снилс в процессе импорта ps
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    /// <returns>
    /// список снилсов
    /// </returns>
    IList<string> GetExportingData(Guid batchId);

    #endregion
  }
}