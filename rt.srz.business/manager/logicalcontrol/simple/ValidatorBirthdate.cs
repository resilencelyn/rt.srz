// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBirthdate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator birthdate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step2;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The validator birthdate.
  /// </summary>
  public class ValidatorBirthdate : CheckTextProperty<FaultBirthdateException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorBirthdate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorBirthdate(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Birthday, Resource.Date)
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
        return Resourcessrz.CaptionValidatorBirthdate;
      }
    }
    public override void CheckObject(model.srz.Statement statement)
    {
      base.CheckObject(statement);
    }

    #endregion
  }
}