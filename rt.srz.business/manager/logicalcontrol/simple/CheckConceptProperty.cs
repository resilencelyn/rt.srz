// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckConceptProperty.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The check concept property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq.Expressions;

  using NHibernate;

  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  /// The check concept property.
  /// </summary>
  /// <typeparam name="TException">
  /// </typeparam>
  public abstract class CheckConceptProperty<TException> : Check
    where TException : LogicalControlException, new()
  {
    #region Fields

    /// <summary>
    /// The deleg.
    /// </summary>
    private readonly Func<Statement, object> deleg;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckConceptProperty{TException}"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    protected CheckConceptProperty(ISessionFactory sessionFactory, Expression<Func<Statement, object>> expression)
      : base(CheckLevelEnum.Simple, sessionFactory, expression)
    {
      deleg = expression.Compile();
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// </exception>
    /// <exception cref="TException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      try
      {
        // if (deleg(statement) == null || !(deleg(statement) is int) || (int)deleg(statement) <= 0)
        if (deleg != null && (int)deleg(statement) <= 0)
        {
          throw new TException();
        }
      }
      catch (NullReferenceException)
      {
        throw new TException();
      }
    }

    #endregion
  }
}