// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusEnforceable.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The status enforceable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.status
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The status enforceable.
  /// </summary>
  public class StatusEnforceable : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusEnforceable" /> class.
    /// </summary>
    public StatusEnforceable()
      : base(StatusStatement.Enforceable)
    {
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The is edit.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public override bool IsEdit(Expression<Func<Statement, object>> expression)
    {
      return false;
    }

    #endregion
  }
}