// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorCategory.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator category.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step2;

  #endregion

  /// <summary>
  ///   The validator category.
  /// </summary>
  public class ValidatorCategory : CheckConceptProperty<FaultCategoryException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorCategory"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorCategory(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.InsuredPersonData.Category.Id)
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
        return Resource.CaptionValidatorCategory;
      }
    }

    #endregion
  }
}