// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorEmail.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator email.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions.step3;
  using rt.srz.model.Properties;
  using rt.srz.model.srz;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The validator email.
  /// </summary>
  public class ValidatorEmail : CheckTextProperty<FaultEmailException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorEmail"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorEmail(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.ContactInfo.Email, Resource.RegexEmail)
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
        return Resourcessrz.CaptionValidatorEmail;
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
      if (statement.ContactInfo == null)
      {
        return;
      }

      if (string.IsNullOrEmpty(statement.ContactInfo.Email))
      {
        return;
      }

      base.CheckObject(statement);
    }

    #endregion
  }
}