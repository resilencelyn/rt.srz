// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckStatementManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The check statement factory.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol
{
  #region

  using System;
  using System.Linq;
  using System.Linq.Expressions;

  using rt.srz.business.interfaces.logicalcontrol;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The check statement factory.
  /// </summary>
  public class CheckStatementManager : ICheckManager
  {
    #region Fields

    /// <summary>
    ///   The check statements.
    /// </summary>
    private readonly ICheckStatement[] checkStatements;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckStatementManager"/> class.
    /// </summary>
    /// <param name="checkStatements">
    /// The check statements.
    /// </param>
    public CheckStatementManager(ICheckStatement[] checkStatements)
    {
      this.checkStatements = checkStatements;
    }

    #endregion

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
    /// <exception cref="LogicalControlException">
    /// </exception>
    public void CheckProperty(Statement statement, Expression<Func<Statement, object>> expression)
    {
      LogicalControlException ex = null;
      var checks = checkStatements.Where(x => x.Expression != null && x.Expression.ToString() == expression.ToString());
      foreach (var checkStatement in checks)
      {
        try
        {
          if (checkStatement.CheckRequired)
          {
            checkStatement.CheckObject(statement);
          }
        }
        catch (LogicalControlException exception)
        {
          if (ex == null)
          {
            ex = exception;
          }
          else
          {
            ex.AddException(exception);
          }
        }
      }

      if (ex != null)
      {
        throw ex;
      }
    }

    /// <summary>
    /// The check statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <param name="level">
    /// </param>
    public void CheckStatement(Statement statement, CheckLevelEnum level)
    {
      LogicalControlException ex = null;
      foreach (var checkStatement in checkStatements.Where(x => x.Level == level))
      {
        try
        {
          if (checkStatement.CheckRequired)
          {
            checkStatement.CheckObject(statement);
          }
        }
        catch (LogicalControlException exception)
        {
          if (ex == null)
          {
            ex = exception;
          }
          else
          {
            ex.AddException(exception);
          }
        }
      }

      if (ex != null)
      {
        throw ex;
      }
    }

    #endregion
  }
}