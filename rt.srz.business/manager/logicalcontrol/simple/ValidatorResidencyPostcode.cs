// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorResidencyPostcode.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator residency postcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step3;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator residency postcode.
  /// </summary>
  public class ValidatorResidencyPostcode : Check
  {
    #region Fields

    /// <summary>
    ///   The deleg.
    /// </summary>
    private readonly Func<Statement, object> deleg;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorResidencyPostcode"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorResidencyPostcode(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.Address2.Postcode)
    {
      deleg = Expression.Compile();
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the caption.
    /// </summary>
    public override string Caption
    {
      get
      {
        return Resource.CaptionValidatorResidencyPostcode;
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
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      if (statement.Address2 == null)
      {
        return;
      }

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      try
      {
        var postcode = (string)deleg(statement);
        if (string.IsNullOrEmpty(postcode))
        {
          throw new FaultPostcodeException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultPostcodeException();
      }
    }

    #endregion
  }
}