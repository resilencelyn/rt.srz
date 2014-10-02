// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBirthdateFuture.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;

  #endregion

  /// <summary>
  /// The validator birthdate future.
  /// </summary>
  public class ValidatorBirthdateFuture : CheckDateFutureProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorBirthdateFuture"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorBirthdateFuture(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Birthday, "Дата рождения ")
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
        return Resource.CaptionValidatorBirthdateFuture;
      }
    }

    #endregion
  }
}