// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorTemporaryCertificateNumberExists.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator temporary certificate number exists.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The validator temporary certificate number exists.
  /// </summary>
  public class ValidatorTemporaryCertificateNumberExists : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorTemporaryCertificateNumberExists"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorTemporaryCertificateNumberExists(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Complex, sessionFactory)
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
        return Resource.CaptionValidatorTemporaryCertificateNumberExists;
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
    /// <exception cref="FaultTemporaryCertificateNumberExists">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      var number = statement.NumberTemporaryCertificate;

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      if (string.IsNullOrEmpty(number))
      {
        return;
      }

      MedicalInsurance m = null;
      var count =
        session.QueryOver<Statement>()
               .JoinAlias(x => x.MedicalInsurances, () => m)
               .Where(x => m.PolisType.Id == PolisType.В)
               .And(x => m.PolisNumber == number)
               .And(x => x.Id != statement.Id)
               .And(x => x.Status.Id != StatusStatement.Cancelled)
               .And(x => x.Status.Id != StatusStatement.Declined)
               .RowCount();

      if (count > 0)
      {
        throw new FaultTemporaryCertificateNumberExists();
      }
    }

    #endregion
  }
}