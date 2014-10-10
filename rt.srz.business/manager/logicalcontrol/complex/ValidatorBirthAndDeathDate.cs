// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBirthAndDeathDate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator birth and death date.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator birth and death date.
  /// </summary>
  public class ValidatorBirthAndDeathDate : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorBirthAndDeathDate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorBirthAndDeathDate(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Complex, sessionFactory)
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
        return Resource.CaptionValidatorBirthAndDeathDate;
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
      if (statement.InsuredPerson.Status != null && statement.InsuredPerson.Status.Id == StatusPerson.Dead
          && statement.InsuredPerson.DeadInfo != null && statement.InsuredPersonData.Birthday != null
          && statement.InsuredPersonData.Birthday.Value > statement.InsuredPerson.DeadInfo.DateDead)
      {
        throw new FaultBirthdateLargerDeathdateException();
      }
    }

    #endregion
  }
}