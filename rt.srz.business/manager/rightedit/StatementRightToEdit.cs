// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementRightToEdit.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement right to edit.
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
  ///   The statement right to edit.
  /// </summary>
  public abstract class StatementRightToEdit : IStatementRightToEdit
  {
    #region Fields

    /// <summary>
    ///   The property.
    /// </summary>
    private readonly int property;

    /// <summary>
    ///   The editing property.
    /// </summary>
    private List<string> editingProperty;

    /// <summary>
    ///   The not editing property.
    /// </summary>
    private List<string> notEditingProperty;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="StatementRightToEdit"/> class.
    /// </summary>
    /// <param name="property">
    /// The property.
    /// </param>
    protected StatementRightToEdit(int property)
    {
      this.property = property;
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets the editing property.
    /// </summary>
    protected List<string> EditingProperty
    {
      get
      {
        return editingProperty ?? (editingProperty = EditingPropertyExpression.Select(x => x.ToString()).ToList());
      }
    }

    /// <summary>
    ///   Gets the editing property expression.
    /// </summary>
    protected virtual List<Expression<Func<Statement, object>>> EditingPropertyExpression
    {
      get
      {
        return new List<Expression<Func<Statement, object>>>();
      }
    }

    /// <summary>
    ///   Gets the not editing property.
    /// </summary>
    protected List<string> NotEditingProperty
    {
      get
      {
        return notEditingProperty
               ?? (notEditingProperty = NotEditingPropertyExpression.Select(x => x.ToString()).ToList());
      }
    }

    /// <summary>
    ///   Gets the not editing property expression.
    /// </summary>
    protected virtual List<Expression<Func<Statement, object>>> NotEditingPropertyExpression
    {
      get
      {
        return new List<Expression<Func<Statement, object>>>();
      }
    }

    #endregion

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
    public bool ApplyTo(int pr)
    {
      return property == pr;
    }

    /// <summary>
    /// The is edit.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public virtual bool IsEdit(Expression<Func<Statement, object>> expression)
    {
      return !NotEditingProperty.Contains(expression.ToString());
    }

    #endregion
  }
}