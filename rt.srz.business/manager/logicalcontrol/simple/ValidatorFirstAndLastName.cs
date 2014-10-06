// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorFirstAndLastName.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator first and last name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator first and last name.
  /// </summary>
  public class ValidatorFirstAndLastName : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorFirstAndLastName"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorFirstAndLastName(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.InsuredPersonData.FirstName)
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
        return Resource.CaptionValidatorFirstAndLastName;
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
    /// <exception cref="FaultFiException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      try
      {
        if (string.IsNullOrEmpty(statement.InsuredPersonData.FirstName)
            && string.IsNullOrEmpty(statement.InsuredPersonData.LastName))
        {
          throw new FaultFiException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultFiException();
      }
    }

    #endregion
  }
}