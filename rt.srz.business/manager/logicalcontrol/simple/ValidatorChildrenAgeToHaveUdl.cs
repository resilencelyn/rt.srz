// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorChildrenAgeToHaveUdl.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator children age to have udl.
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
  ///   The validator children age to have udl.
  /// </summary>
  public class ValidatorChildrenAgeToHaveUdl : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorChildrenAgeToHaveUdl"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorChildrenAgeToHaveUdl(ISessionFactory sessionFactory)
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
        return Resource.CaptionValidatorChildrenAgeToHaveUdl;
      }
    }

    /// <summary>
    ///   Отобображать проверку или нет в списке на странице
    /// </summary>
    public override bool Visible
    {
      get
      {
        return true;
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

      if (statement.DateFiling != null && statement.InsuredPersonData != null && statement.DocumentUdl != null
          && statement.DocumentUdl.DocumentType != null
          && Age.CalculateAgeOnDate(statement.InsuredPersonData.Birthday.Value, statement.DateFiling.Value) < 14
          && statement.DocumentUdl.DocumentType.Id == DocumentType.PassportRf)
      {
        throw new FaultChildrenAgeToHaveUdlException();
      }
    }

    #endregion
  }
}