// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The utils.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services
{
  #region

  using System;
  using System.Linq.Expressions;

  using Serialize.Linq.Extensions;
  using Serialize.Linq.Nodes;

  #endregion

  /// <summary>
  ///   The utils.
  /// </summary>
  public static class Utils
  {
    #region Constants

    /// <summary>
    ///   The c_ admin code.
    /// </summary>
    public const int C_AdminCode = 1;

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get expression node.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="ExpressionNode"/> .
    /// </returns>
    public static ExpressionNode GetExpressionNode(Expression<Func<model.srz.Statement, object>> expression)
    {
      return expression.ToExpressionNode();
    }

    #endregion
  }
}