// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorCauseFiling.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator cause filing.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step1;

  #endregion

  /// <summary>
  ///   The validator cause filing.
  /// </summary>
  public class ValidatorCauseFiling : CheckConceptProperty<FaultCauseFilingException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorCauseFiling"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorCauseFiling(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.CauseFiling.Id)
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
        return Resource.CaptionValidatorCauseFiling;
      }
    }

    #endregion
  }
}