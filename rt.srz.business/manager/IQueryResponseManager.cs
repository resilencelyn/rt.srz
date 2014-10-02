//-------------------------------------------------------------------------------------
// <copyright file="IQueryResponseManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

 

  /// <summary>
  /// The interface QueryResponseManager.
  /// </summary>
  public partial interface IQueryResponseManager
  {
    /// <summary>
    /// Данные для ненайденных снилс в процессе импорта ps
    /// </summary>
    /// <param name="batchId"></param>
    /// <returns>список снилсов</returns>
    IList<string> GetExportingData(Guid batchId);
  }
}