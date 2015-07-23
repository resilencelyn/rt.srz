// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorReinsuranceRules.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator reinsurance rules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  using System.Linq;

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step1;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The validator reinsurance rules.
  /// </summary>
  public class ValidatorReinsuranceRules : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorReinsuranceRules"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorReinsuranceRules(ISessionFactory sessionFactory)
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
        return Resource.CaptionValidatorReinsuranceRules;
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

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var oldStatementList =
        session.QueryOver<Statement>()
               .Where(x => x.InsuredPerson.Id == statement.InsuredPerson.Id)
               .And(x => x.Id != statement.Id)
               .And(x => x.Status.Id != StatusStatement.Cancelled)
               .And(x => x.Status.Id != StatusStatement.Declined)
               .List();

      var maxDate = oldStatementList.Max(x => x.DateFiling);
      if (maxDate.HasValue && maxDate.Value.Date >= statement.DateFiling.Value.Date)
      {
        throw new FaultDateFillingLessThenLastStatement();
      }
    }

    #endregion
  }
}