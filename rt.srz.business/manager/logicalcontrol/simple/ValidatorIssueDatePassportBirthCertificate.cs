// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorIssueDatePassportBirthCertificate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator issue date passport birth certificate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator issue date passport birth certificate.
  /// </summary>
  public class ValidatorIssueDatePassportBirthCertificate : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorIssueDatePassportBirthCertificate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorIssueDatePassportBirthCertificate(ISessionFactory sessionFactory)
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
        return Resource.CaptionValidatorIssueDatePassportBirthCertificate;
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

      if (statement.InsuredPersonData.Birthday == null)
      {
        return;
      }

      if (statement.DateFiling == null)
      {
        return;
      }

      var requestDate = (DateTime)statement.DateFiling;

      if (statement.DocumentUdl != null && statement.DocumentUdl.DocumentType != null)
      {
        if (statement.InsuredPersonData.Citizenship != null && statement.InsuredPersonData.Citizenship.Id == Country.RUS
            && statement.DocumentUdl.DateIssue.HasValue)
        {
          var ageDrDocdt = Age.CalculateAgeOnDate(
                                                  statement.InsuredPersonData.Birthday.Value, 
                                                  statement.DocumentUdl.DateIssue.Value);
          var ageDr30Dbeg = Age.CalculateAgeOnDate(
                                                   statement.InsuredPersonData.Birthday.Value.AddDays(30), 
                                                   statement.DateFiling.Value);

          // 1. Свидетельство о рождении может указываться только для граждан, чей возраст на дату подачи заявления не превышает 14 лет плюс 30 календарных дней; 
          if ((statement.DocumentUdl.DocumentType.Id == DocumentType.BirthCertificateRf
               || statement.DocumentUdl.DocumentType.Id == DocumentType.DocumentType24) && ageDrDocdt >= 14)
          {
            throw new FaultBirthCertificateException();
          }

          if (statement.DocumentUdl.DocumentType.Id == DocumentType.PassportRf
              && Age.CalculateAgeOnDate(
                                        statement.InsuredPersonData.Birthday.Value, 
                                        statement.DocumentUdl.DateIssue.Value) < 14)
          {
            throw new FaultDateIssueDocumentUdl();
          }

          // 2. Дата выдачи паспорта гражданина РФ, чей возраст на дату подачи заявления составляет от 20 до 45 лет, должна быть больше даты рождения не менее, чем на 20 лет плюс 30 календарных дней; 
          if (statement.DocumentUdl.DocumentType.Id == DocumentType.PassportRf
              && (20 <= ageDr30Dbeg && ageDr30Dbeg <= 44) && ageDrDocdt < 20)
          {
            throw new FaultIssueDate20Exception();
          }

          // 3. Дата выдачи паспорта гражданина РФ, чей возраст на дату подачи заявления больше 45 лет, должна быть больше даты рождения не менее, чем на 45 лет плюс 30 календарных дней.
          if (statement.DocumentUdl.DocumentType.Id == DocumentType.PassportRf && ageDr30Dbeg >= 45 && ageDrDocdt < 45)
          {
            throw new FaultIssueDate45Exception();
          }
        }
      }
    }

    #endregion
  }
}