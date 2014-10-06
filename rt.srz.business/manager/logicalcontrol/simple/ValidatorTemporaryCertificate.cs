// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorTemporaryCertificate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The temporary certificate check.
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
  ///   The temporary certificate check.
  /// </summary>
  public class ValidatorTemporaryCertificate : CheckTextProperty<FaultTemporaryCertificateFormatException>
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorTemporaryCertificate"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorTemporaryCertificate(ISessionFactory sessionFactory)
      : base(sessionFactory, x => x.MedicalInsurances[0].PolisNumber, Resource.RegexOnlyNumber)
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
        return Resourcessrz.CaptionValidatorTemporaryCertificate;
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

      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      // если не требуется выдача полиса, то поля не проверяем
      if (!statement.AbsentPrevPolicy.HasValue || !statement.AbsentPrevPolicy.Value)
      {
        return;
      }

      try
      {
        var temp = statement.MedicalInsurances.FirstOrDefault(x => x.PolisType.Id == PolisType.В);
        if (temp == null || string.IsNullOrEmpty(temp.PolisNumber))
        {
          throw new FaultTemporaryCertificateEmptyException();
        }

        // Проверяем на длину
        if (temp.PolisNumber.Length != 9)
        {
          throw new FaultTemporaryCertificateWrongLengthException();
        }

        // Проверяем на формат
        if (!Regex.IsMatch(temp.PolisNumber))
        {
          throw new FaultTemporaryCertificateFormatException();
        }
      }
      catch (NullReferenceException)
      {
        throw new FaultTemporaryCertificateEmptyException();
      }
    }

    #endregion
  }
}