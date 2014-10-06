// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusExercised.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The status exercised.
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
  ///   The status exercised.
  /// </summary>
  public class StatusExercised : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusExercised" /> class.
    /// </summary>
    public StatusExercised()
      : base(StatusStatement.Exercised)
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