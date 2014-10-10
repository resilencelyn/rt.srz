// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorNewEnp.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The enp check.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.algorithms;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step1;
  using rt.srz.model.logicalcontrol.exceptions.step6.issue;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The enp check.
  /// </summary>
  public class ValidatorNewEnp : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorNewEnp"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorNewEnp(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.MedicalInsurances[1].Enp)
    {
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
        return Resource.CaptionValidatorNewEnp;
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

      // Проверка соответствия полу, дате рождения и контрольному разряду
      var policy = statement.MedicalInsurances.LastOrDefault(x => x.PolisType.Id != PolisType.В);

      if (!(statement.PolicyIsIssued.HasValue && statement.PolicyIsIssued.Value))
      {
        return;
      }

      if (policy == null)
      {
        throw new FaultNewEnpException();
      }

      if (!string.IsNullOrEmpty(policy.Enp))
      {
        if (!EnpChecker.CheckIdentifier(policy.Enp))
        {
          throw new FaultEnpException();
        }

        if (statement.InsuredPersonData != null && statement.InsuredPersonData.Birthday != null
            && statement.InsuredPersonData.Gender != null)
        {
          if (
            !EnpChecker.CheckBirthdayAndGender(
                                               policy.Enp, 
                                               statement.InsuredPersonData.Birthday.Value, 
                                               statement.InsuredPersonData.Gender.Id == Sex.Sex1))
          {
            throw new FaultNewEnpBirthdayAndGenderException();
          }
        }
      }
      else
      {
        throw new FaultEnpException();
      }
    }

    #endregion
  }
}