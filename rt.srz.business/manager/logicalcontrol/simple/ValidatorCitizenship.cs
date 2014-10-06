// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorCitizenship.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator citizenship.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator citizenship.
  /// </summary>
  public class ValidatorCitizenship : CheckConceptProperty<FaultCitizenshipException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorCitizenship"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorCitizenship(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Citizenship.Id)
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
        return Resource.CaptionValidatorCitizenship;
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
      if (!statement.InsuredPersonData.IsNotCitizenship)
      {
        base.CheckObject(statement);
      }
    }

    #endregion
  }
}