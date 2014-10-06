// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICheckManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The CheckStatementManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.interfaces.logicalcontrol
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The CheckStatementManager interface.
  /// </summary>
  public interface ICheckManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The check property.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    void CheckProperty(Statement statement, Expression<Func<Statement, object>> expression);

    /// <summary>
    /// The check statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="level">
    /// The level.
    /// </param>
    void CheckStatement(Statement statement, CheckLevelEnum level);

    #endregion
  }
}