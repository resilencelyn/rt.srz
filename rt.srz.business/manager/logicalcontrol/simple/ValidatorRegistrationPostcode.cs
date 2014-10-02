// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRegistrationPostcode.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator registration postcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step3;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator registration postcode.
  /// </summary>
  public class ValidatorRegistrationPostcode : Check
  {
    private readonly Func<Statement, object> deleg;

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRegistrationPostcode"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorRegistrationPostcode(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.Address.Postcode)
    {
      deleg = Expression.Compile();
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
        return Resource.CaptionValidatorRegistrationPostcode;
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
    /// <exception cref="ArgumentNullException">
    /// </exception>
    /// <exception cref="FaultPostcodeException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      if (statement == null)
      {
        throw new ArgumentNullException("statement");
      }

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      // Если бомж, то проверку пропускаем
      if (statement.Address.IsHomeless.HasValue && statement.Address.IsHomeless.Value)
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