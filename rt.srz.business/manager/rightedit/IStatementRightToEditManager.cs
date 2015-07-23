// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementRightToEditManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementRightToEditManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The StatementRightToEditManager interface.
  /// </summary>
  public interface IStatementRightToEditManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The is right to edit.
    /// </summary>
    /// <param name="propertys">
    /// The propertys.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IsRightToEdit(IEnumerable<Concept> propertys, Expression<Func<Statement, object>> expression);

    #endregion
  }
}