// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorPolisCertificateDateIssue.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The validator polis certificate date issue.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.simple
{
  #region

  using System;
  using System.Linq;
  using NHibernate;
  using rt.srz.business.Properties;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.logicalcontrol.exceptions.step6.issue;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The validator polis certificate date issue.
  /// </summary>
  public class ValidatorPolisCertificateDateIssue : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorPolisCertificateDateIssue"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory. 
    /// </param>
    public ValidatorPolisCertificateDateIssue(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Simple, sessionFactory, x => x.MedicalInsurances[1].DateFrom)
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
        return Resource.CaptionValidatorPolisCertificateDateIssue;
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
      try
      {
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

          var policy = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
          if (policy == null)
            throw new FaultPolisCertificateEmptyException();
          
          var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
          if (temp == null)
            throw new FaultTemporaryCertificateEmptyException();
          
          // Если на Шаг 1 УСТАНОВЛЕН флажок "Требуется выдача нового полиса", 
          // то дата заявление <=  дата выдачи ВС <= дата выдачи полиса 

          // проверяем чтобы дата выдачи полиса была меньше даты подачи заявления
          if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization && statement.DateFiling != null && policy.DateFrom.Date < statement.DateFiling.Value.Date)
          {
            throw new FaultPoliceCertificateDateException();
          }

          // проверяем чтобы дата выдачи полиса была меньше даты выдачи временного свидетельства
          if (statement.CauseFiling != null && statement.CauseFiling.Id != CauseReinsurance.Initialization && policy.DateFrom.Date < temp.DateFrom.Date)
          {
            throw new FaultPoliceCertificateDateException();
          }
        }
        else
        {
          var policy = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
          if (policy == null)
          {
            throw new FaultPolisCertificateEmptyException();
          }
          
          // проверяем чтобы дата выдачи полиса была больше даты подачи заявления
          if (statement.DateFiling != null && policy.DateFrom.Date > statement.DateFiling.Value.Date)
          {
            throw new FaultPoliceCertificateDateNotNeyPolisException();
          }
        }

        var pol = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id != PolisType.В);
        if (pol == null)
          throw new FaultPolisCertificateEmptyException();
        
        // проверяем чтобы дата выдачи полиса была не в будущем времени
        if (pol.DateFrom.Date > DateTime.Now.Date)
        {
          throw new FaultPoliceCertificateFutureException();
        }

        // проверяем, чтобы дата выдачи полиса была меньше даты окончания выдачи полиса
        // это возможно в ситуации, когда заявитель имеет документ с ограниченным сроком действия
        if (pol.DateFrom.Date > pol.DateTo.Date)
        {
          throw new FaultPoliceCertificateDateFromMoreThenDateEnd();
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