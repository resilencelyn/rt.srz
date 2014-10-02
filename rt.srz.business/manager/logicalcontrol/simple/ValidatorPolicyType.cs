// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorPolicyType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator policy type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The validator policy type.
  /// </summary>
  public class ValidatorPolicyType : CheckConceptProperty<FaultPolicyTypeException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorPolicyType"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorPolicyType(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.FormManufacturing.Id)
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
        return Resource.CaptionValidatorPolicyType;
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
      base.CheckObject(statement);

      // if (statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value)
      // {
      // 	base.CheckObject(statement);
      // }
    }

    #endregion
  }
}