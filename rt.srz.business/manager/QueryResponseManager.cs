// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponseManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The QueryResponseManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using NHibernate;
  using NHibernate.SqlCommand;

  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The QueryResponseManager.
  /// </summary>
  public partial class QueryResponseManager
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
    public IList<string> GetExportingData(Guid batchId)
    {
      // batchId = Guid.Parse("CDB3902F-DD82-49C4-84D0-A281011D6964");
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      EmploymentHistory hist = null;
      Message m = null;
      var query =
        session.QueryOver<QueryResponse>()
               .JoinAlias(x => x.EmploymentHistories, () => hist, JoinType.LeftOuterJoin)
               .JoinAlias(x => x.Message, () => m)
               .WhereRestrictionOn(() => hist.Id)
               .IsNull.Where(x => m.Batch.Id == batchId)
               .Select(x => x.Snils)
               .RootCriteria.SetTimeout(int.MaxValue)
               .List<string>();

      return query;
    }

    #endregion
  }
}