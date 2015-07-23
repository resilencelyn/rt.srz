// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBirthplace.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator birthplace.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.Properties;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The validator birthplace.
  /// </summary>
  public class ValidatorBirthplace : CheckTextProperty<FaultBirthplaceException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorBirthplace"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorBirthplace(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Birthplace, Resource.RegexBirthplace)
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
        return Resourcessrz.CaptionValidatorBirthplace;
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

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      base.CheckObject(statement);

      if (string.IsNullOrEmpty(statement.InsuredPersonData.Birthplace))
      {
        throw new FaultBirthplaceException();
      }
    }

    #endregion
  }
}