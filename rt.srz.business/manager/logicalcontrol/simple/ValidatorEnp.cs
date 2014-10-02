// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorEnp.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The enp check.
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
  using rt.srz.model.logicalcontrol.exceptions.step1;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The enp check.
  /// </summary>
  public class ValidatorEnp : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorEnp"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorEnp(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.NumberPolicy)
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
        return Resource.CaptionValidatorEnp;
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

      if (statement.NotCheckPolisNumber.HasValue && statement.NotCheckPolisNumber.Value)
      {
        return;
      }

      // Если нужно выдать новый полис и полис выдан, то обязательно надо заполнить номер полиса
      ////if (statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value && statement.PolicyIsIssued.HasValue
      ////    && statement.PolicyIsIssued.Value && string.IsNullOrEmpty(statement.NumberPolicy))
      ////{
      ////  throw new FaultEnpException();
      ////}

      // Проверка соответствия полу, дате рождения и контрольному разряду
      if (!string.IsNullOrEmpty(statement.NumberPolicy))
      {
        if (!EnpChecker.CheckIdentifier(statement.NumberPolicy))
        {
          throw new FaultEnpException();
        }

        if (statement.InsuredPersonData != null && statement.InsuredPersonData.Birthday != null
            && statement.InsuredPersonData.Gender != null)
        {
          if (
            !EnpChecker.CheckBirthdayAndGender(
              statement.NumberPolicy,
              statement.InsuredPersonData.Birthday.Value,
              statement.InsuredPersonData.Gender.Id == Sex.Sex1))
          {
            throw new FaultEnpBirthdayAndGenderException();
          }
        }
      }

      ////// Если новый полис выдавать не требуется, то ЕНП должен быть известен, он на полисе, который предъявили
      ////if ((!statement.AbsentPrevPolicy.HasValue || statement.AbsentPrevPolicy == false)
      ////    && string.IsNullOrEmpty(statement.NumberPolicy))
      ////{
      ////  throw new FaultEnpAbsentPrevPolicyException();
      ////}
    }

    #endregion
  }
}