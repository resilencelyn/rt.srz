// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorGenderConformityMiddleName.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator gender conformity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  using AutoComplete = rt.srz.model.srz.AutoComplete;

  /// <summary>
  ///   The validator gender conformity.
  /// </summary>
  public class ValidatorGenderConformityMiddleName : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorGenderConformityMiddleName"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorGenderConformityMiddleName(ISessionFactory sessionFactory)
      : base(CheckLevelEnum.Complex, sessionFactory, x => x.InsuredPersonData.Gender.Id)
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
        return Resource.CaptionValidatorGenderConformityMiddleName;
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
      if (statement.InsuredPersonData.Gender == null)
      {
        throw new FaultGenderException();
      }

      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      Concept concept1 = null;

      // Пропускам проверку отчества если оно не указано
      if (!string.IsNullOrEmpty(statement.InsuredPersonData.MiddleName))
      {
        var query =
          session.QueryOver<AutoComplete>()
                 .JoinAlias(x => x.Gender, () => concept1)
                 .Where(x => x.Name == statement.InsuredPersonData.MiddleName)
                 .And(x => concept1.Code == statement.InsuredPersonData.Gender.Code)
                 .RowCount();
        if (query == 0)
        {
          throw new FaultGenderConformityMiddleNameException();
        }
      }
    }

    #endregion
  }
}