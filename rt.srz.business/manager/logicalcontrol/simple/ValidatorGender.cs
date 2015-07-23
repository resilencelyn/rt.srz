// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorGender.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator gender.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step2;

  #endregion

  /// <summary>
  ///   The validator gender.
  /// </summary>
  public class ValidatorGender : CheckConceptProperty<FaultGenderException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorGender"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorGender(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Gender.Id)
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
        return Resource.CaptionValidatorGender;
      }
    }

    #endregion
  }
}