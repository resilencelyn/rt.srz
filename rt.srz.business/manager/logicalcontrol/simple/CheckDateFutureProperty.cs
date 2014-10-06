// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckDateFutureProperty.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The check text property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq.Expressions;

  using NHibernate;

  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The check text property.
  /// </summary>
  public abstract class CheckDateFutureProperty : Check
  {
    #region Fields

    /// <summary>
    /// The deleg.
    /// </summary>
    private readonly Func<Statement, object> deleg;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckDateFutureProperty"/> class. 
    /// Initializes a new instance of the <see cref="CheckDateFutureProperty{TException}"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <param name="name">
    /// The name.
    /// </param>
    protected CheckDateFutureProperty(
      ISessionFactory sessionFactory, 
      Expression<Func<Statement, object>> expression, 
      string name)
      : base(CheckLevelEnum.Simple, sessionFactory, expression)
    {
      Name = name;
      deleg = expression.Compile();
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets the name.
    /// </summary>
    protected string Name { get; set; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check object.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      try
      {
        var value = deleg(statement);
        if (value != null)
        {
          var date = (DateTime)value;
          if (date >= DateTime.Today.AddDays(1))
          {
            throw new FaultDateFutureException(Name);
          }
        }
      }
      catch (NullReferenceException)
      {
      }
    }

    #endregion
  }
}