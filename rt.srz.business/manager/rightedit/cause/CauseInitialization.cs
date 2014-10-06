// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseInitialization.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause initialization.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause
{
  #region references

  using System;
  using System.Linq.Expressions;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause initialization.
  /// </summary>
  public class CauseInitialization : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseInitialization" /> class.
    /// </summary>
    public CauseInitialization()
      : base(CauseReinsurance.Initialization)
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