// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementNotEdit.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement not edit.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit
{
  using System;
  using System.Linq.Expressions;

  using rt.srz.model.srz;

  /// <summary>
  ///   The statement not edit.
  /// </summary>
  public class StatementNotEdit : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StatementNotEdit"/> class.
    /// </summary>
    public StatementNotEdit()
      : base(-10)
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
      return true;
    }

    #endregion
  }
}