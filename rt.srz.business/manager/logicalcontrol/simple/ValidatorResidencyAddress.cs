// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorResidencyAddress.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator residency address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator residency address.
  /// </summary>
  public class ValidatorResidencyAddress : CheckAddressProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorResidencyAddress"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorResidencyAddress(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.Address2)
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
        return Resource.CaptionValidatorResidencyAddress;
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
      if (statement.Address2 != null)
      {
        base.CheckObject(statement);
      }
    }

    #endregion
  }
}