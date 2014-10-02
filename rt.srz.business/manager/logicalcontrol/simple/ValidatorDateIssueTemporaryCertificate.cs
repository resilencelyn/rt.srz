// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorDateIssueTemporaryCertificate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator date issue temporary certificate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.configuration.algorithms;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The validator date issue temporary certificate.
  /// </summary>
  public class ValidatorDateIssueTemporaryCertificate : CheckTextProperty<FaultDateIssueTemporaryCertificateException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorDateIssueTemporaryCertificate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorDateIssueTemporaryCertificate(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.MedicalInsurances[0].DateFrom, Resource.Date)
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
        return Resourcessrz.CaptionValidatorDateIssueTemporaryCertificate;
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

      if (statement.InsuredPersonData != null
          && statement.DateIssueTemporaryCertificate.HasValue
          && statement.InsuredPersonData.Birthday.HasValue
          && statement.DateIssueTemporaryCertificate < statement.InsuredPersonData.Birthday)
      {
        throw new FaultDateIssueTemporaryCertificateLessBirthdateException();
      }

      if (statement.DateFiling.HasValue
        && statement.ResidencyDocument!= null
        && statement.ResidencyDocument.DateExp.HasValue
        && statement.DateFiling.Value> statement.ResidencyDocument.DateExp.Value)
      {
        throw new FaultMedicalInsuranceDateEndLessDateFromException();
      }

      ////if (statement.MedicalInsurances.Where(x => x.PolisType.Id == PolisType.В)
      ////  .Any(medicalInsurance => medicalInsurance.DateTo.Date != DateTymeHelper.CalculateEnPeriodWorkingDay(medicalInsurance.DateFrom, 30).Date))
      ////{
      ////  throw new FaultMedicalInsuranceDateNotEquals30Exception();
      ////}
    }

    #endregion
  }
}