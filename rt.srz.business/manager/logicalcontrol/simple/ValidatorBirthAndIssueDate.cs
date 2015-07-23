// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorBirthAndIssueDate.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator birth and issue date.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator birth and issue date.
  /// </summary>
  public class ValidatorBirthAndIssueDate : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorBirthAndIssueDate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorBirthAndIssueDate(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.DocumentUdl.DateIssue)
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
        return Resource.CaptionValidatorBirthAndIssueDate;
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
      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      if (statement.InsuredPersonData.Birthday > statement.DocumentUdl.DateIssue)
      {
        throw new FaultBirthdateLargerDocumentDateIssueException();
      }

      if (statement.DateFiling != null && statement.InsuredPersonData.Birthday > statement.DateFiling.Value)
      {
        throw new FaultBirthdateLargerDateFillingException();
      }
    }

    #endregion
  }
}