// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStatementRightToEdit.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The StatementRightToEdit interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The StatementRightToEdit interface.
  /// </summary>
  public interface IStatementRightToEdit
  {
    #region Public Methods and Operators

    /// <summary>
    /// The apply to.
    /// </summary>
    /// <param name="pr">
    /// The pr.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool ApplyTo(int pr);

    /// <summary>
    /// The is edit.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IsEdit(Expression<Func<Statement, object>> expression);

    #endregion
  }
}