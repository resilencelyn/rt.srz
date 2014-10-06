// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementSearchManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementSearchManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System.Collections.Generic;

  using rt.core.model.dto;
  using rt.srz.model.dto;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The StatementSearchManager interface.
  /// </summary>
  public interface IStatementSearchManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get insured person by statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="keys">
    /// </param>
    /// <returns>
    /// The <see cref="InsuredPerson"/>.
    /// </returns>
    InsuredPerson GetInsuredPersonByStatement(Statement statement, IEnumerable<SearchKey> keys);

    /// <summary>
    /// </summary>
    /// <param name="keys">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<InsuredPerson> GetInsuredPersonsByKeys(IEnumerable<SearchKey> keys);

    /// <summary>
    /// Осуществляет поиск заявлений по заданному критерию
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    SearchResult<SearchStatementResult> Search(SearchStatementCriteria criteria);

    #endregion
  }
}