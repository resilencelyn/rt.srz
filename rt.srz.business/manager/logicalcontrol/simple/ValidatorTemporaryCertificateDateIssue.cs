// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorTemporaryCertificateDateIssue.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator temporary certificate date issue.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator temporary certificate date issue.
  /// </summary>
  public class ValidatorTemporaryCertificateDateIssue : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorTemporaryCertificateDateIssue"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorTemporaryCertificateDateIssue(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.MedicalInsurances[0].DateFrom)
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
        return Resource.CaptionValidatorTemporaryCertificateDateIssue;
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
      // если не требуется выдача полиса, то поля не проверяем
      if (!statement.AbsentPrevPolicy.HasValue || !statement.AbsentPrevPolicy.Value)
      {
        return;
      }

      var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
      if (temp == null)
      {
        return;
      }

      if (temp.DateFrom < new DateTime(2011, 5, 1))
      {
        throw new FaultTemporaryCertificateDateIssueException();
      }

      if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization
          && statement.DateFiling != null && temp.DateFrom.Date < statement.DateFiling.Value.Date)
      {
        throw new FaultTemporaryCertificateDateIssueUnderDateStatementException();
      }

      if (temp.DateFrom.Date > DateTime.Now.Date)
      {
        throw new FaultTemporaryCertificateDateIssueBiggerThenNow();
      }
    }

    #endregion
  }
}