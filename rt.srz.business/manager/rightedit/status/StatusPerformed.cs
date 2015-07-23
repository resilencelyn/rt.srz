// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusPerformed.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Исподняется
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.status
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   Исподняется
  /// </summary>
  public class StatusPerformed : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusPerformed" /> class.
    /// </summary>
    public StatusPerformed()
      : base(StatusStatement.Performed)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the editing property expression.
    /// </summary>
    protected override List<Expression<Func<Statement, object>>> EditingPropertyExpression
    {
      get
      {
        return new List<Expression<Func<Statement, object>>>
               {
                 x => x.PolicyIsIssued, 
                 x => x.MedicalInsurances[1].Enp, 
                 x => x.MedicalInsurances[1].PolisNumber, 
                 x => x.MedicalInsurances[1].DateFrom
               };
      }
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
      return EditingProperty.Contains(expression.ToString());
    }

    #endregion
  }
}