// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementRightToEditManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement right to edit manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The statement right to edit manager.
  /// </summary>
  public class StatementRightToEditManager : IStatementRightToEditManager
  {
    #region Fields

    /// <summary>
    ///   The statement right to edits.
    /// </summary>
    protected readonly IStatementRightToEdit[] StatementRightToEdits;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StatementRightToEditManager"/> class.
    /// </summary>
    /// <param name="statementRightToEdits">
    /// The statement right to edits.
    /// </param>
    public StatementRightToEditManager(IStatementRightToEdit[] statementRightToEdits)
    {
      StatementRightToEdits = statementRightToEdits;
    }

    #endregion

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
    public bool IsRightToEdit(IEnumerable<Concept> propertys, Expression<Func<Statement, object>> expression)
    {
      return
        propertys.All(
                      p =>
                      (StatementRightToEdits.FirstOrDefault(x => x.ApplyTo(p.Id)) ?? new StatementNotEdit()).IsEdit(
                                                                                                                    expression));
    }

    #endregion
  }
}