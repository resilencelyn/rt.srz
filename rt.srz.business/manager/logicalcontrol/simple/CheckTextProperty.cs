// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckTextProperty.cs" company="РусБИТех">
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
  using System.Text.RegularExpressions;

  using NHibernate;

  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  /// The check text property.
  /// </summary>
  /// <typeparam name="TException">
  /// </typeparam>
  public abstract class CheckTextProperty<TException> : Check
    where TException : LogicalControlException, new()
  {
    #region Fields

    /// <summary>
    /// The deleg.
    /// </summary>
    private readonly Func<Statement, object> deleg;

    /// <summary>
    ///   The regex.
    /// </summary>
    private readonly Regex regex;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckTextProperty{TException}"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <param name="pattern">
    /// The pattern.
    /// </param>
    protected CheckTextProperty(
      ISessionFactory sessionFactory, 
      Expression<Func<Statement, object>> expression, 
      string pattern)
      : base(CheckLevelEnum.Simple, sessionFactory, expression)
    {
      Pattern = pattern;
      regex = new Regex(Pattern);
      deleg = expression.Compile();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the pattern.
    /// </summary>
    public string Pattern { get; private set; }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the regex.
    /// </summary>
    protected Regex Regex
    {
      get
      {
        return regex;
      }
    }

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
      try
      {
        var value = deleg(statement);
        if (value != null)
        {
          var str = value.ToString();
          if (!string.IsNullOrEmpty(str) && !regex.IsMatch(str))
          {
            throw new TException();
          }
        }
        else
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