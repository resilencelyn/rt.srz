// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryResponseManager.cs" company="������">
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
    /// ������ ��� ����������� ����� � �������� ������� ps
    /// </summary>
    /// <param name="batchId">
    /// The batch Id.
    /// </param>
    /// <returns>
    /// ������ �������
    /// </returns>
    IList<string> GetExportingData(Guid batchId);

    #endregion
  }
}