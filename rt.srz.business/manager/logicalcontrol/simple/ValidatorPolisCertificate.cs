// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorPolisCertificate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The polis certificate check.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq;

  using NHibernate;

  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.Properties;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using Resourcessrz = rt.srz.business.Properties.Resource;

  #endregion

  /// <summary>
  ///   The polis certificate check.
  /// </summary>
  public class ValidatorPolisCertificate : CheckTextProperty<FaultPolisCertificateFormatException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorPolisCertificate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorPolisCertificate(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.MedicalInsurances[1].PolisNumber, Resource.RegexOnlyNumber)
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
        return Resourcessrz.CaptionValidatorPolisCertificate;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The check.
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

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      // если не требуется выдача полиса
      if (statement.AbsentPrevPolicy.HasValue && statement.AbsentPrevPolicy.Value)
      {
        // если полис не выдан, то не проверяем поля
        if (!statement.PolicyIsIssued.HasValue || !statement.PolicyIsIssued.Value)
        {
          return;
        }
      }

      // В остальных случаях проверяем поле
      try
      {
        var policy = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
        if (policy == null || string.IsNullOrEmpty(policy.PolisNumber))
        {
          throw new FaultPolisCertificateEmptyException();
        }

        switch (policy.PolisType.Id)
        {
            // Для бумажного и электронного полиса длина 11 символов
          case PolisType.П:
          case PolisType.Э:
            if (policy.PolisNumber.Length != 11)
            {
              throw new FaultPolisCertificateWrongLengthException();
            }

            break;

            // Для полиса на УЭК длина 14 символов
          case PolisType.К:
            if (policy.PolisNumber.Length != 14)
            {
              throw new FaultPolisCertificateWrongLengthException();
            }

            break;
        }

        // Проверяем формат
        if (!Regex.IsMatch(policy.PolisNumber))
        {
          throw new FaultPolisCertificateFormatException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultPolisCertificateEmptyException();
      }
    }

    #endregion
  }
}