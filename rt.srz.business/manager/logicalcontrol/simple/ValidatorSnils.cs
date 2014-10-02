// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorSnils.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The snils chack.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.algorithms;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The snils chack.
  /// </summary>
  public class ValidatorSnils : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorSnils"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorSnils(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.InsuredPersonData.Snils)
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorSnils;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check.
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
        if (!string.IsNullOrEmpty(statement.InsuredPersonData.Snils))
        {
          if (statement.InsuredPersonData.NotCheckSnils.HasValue && statement.InsuredPersonData.NotCheckSnils.Value)
          {
            return;
          }

          var res = SnilsChecker.CheckIdentifier(statement.InsuredPersonData.Snils);
          if (!res)
          {
            throw new FaultSnilsException();
          }
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultSnilsException();
      }
    }

    #endregion
  }
}