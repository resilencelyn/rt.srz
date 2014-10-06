// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRepresentativeFirstName.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator representative first name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.Properties;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The validator representative first name.
  /// </summary>
  public class ValidatorRepresentativeFirstName : CheckTextProperty<FaultFirstNameTextException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRepresentativeFirstName"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorRepresentativeFirstName(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.Representative.FirstName, Resource.RegexFio)
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
        return Resourcessrz.CaptionValidatorRepresentativeFirstName;
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

      base.CheckObject(statement);
    }

    #endregion
  }
}