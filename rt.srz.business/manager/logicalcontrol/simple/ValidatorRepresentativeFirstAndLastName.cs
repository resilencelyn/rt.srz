// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRepresentativeFirstAndLastName.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator representative first and last name.
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
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator representative first and last name.
  /// </summary>
  public class ValidatorRepresentativeFirstAndLastName : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRepresentativeFirstAndLastName"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorRepresentativeFirstAndLastName(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.Representative.FirstName)
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
        return Resource.CaptionValidatorRepresentativeFirstAndLastName;
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
      if (statement.ModeFiling.Id != ModeFiling.ModeFiling2)
      {
        return;
      }

      try
      {
        if (string.IsNullOrEmpty(statement.Representative.FirstName)
            && string.IsNullOrEmpty(statement.Representative.LastName))
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