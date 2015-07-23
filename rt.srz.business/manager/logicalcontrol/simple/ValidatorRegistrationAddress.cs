// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorRegistrationAddress.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator registration address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step3;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator registration address.
  /// </summary>
  public class ValidatorRegistrationAddress : CheckAddressProperty
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorRegistrationAddress"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorRegistrationAddress(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.Address)
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
        return Resource.CaptionValidatorRegistrationAddress;
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
      // Если бомж, то проверку пропускаем
      if (statement.Address.IsHomeless.HasValue && statement.Address.IsHomeless.Value)
      {
        return;
      }

      base.CheckObject(statement);

      if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization
          && !statement.Address.DateRegistration.HasValue)
      {
        throw new FaultDateRegistration();
      }
    }

    #endregion
  }
}