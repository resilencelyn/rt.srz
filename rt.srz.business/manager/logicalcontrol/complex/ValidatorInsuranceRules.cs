// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorInsuranceRules.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The validator insurance rules.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step6;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The validator insurance rules.
  /// </summary>
  public class ValidatorInsuranceRules : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidatorInsuranceRules"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public ValidatorInsuranceRules(ISessionFactory sessionFactory)
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
        return Resource.CaptionValidatorInsuranceRules;
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

      if (statement.PointDistributionPolicy == null || statement.PointDistributionPolicy.Parent == null
          || statement.InsuredPerson == null)
      {
        return;
      }

      // если заявления на смену смо, а человек уже принадлежит данной смо
      if (statement.CauseFiling != null && statement.CauseFiling.Oid.Id == Oid.ПричинаподачизаявлениянавыборилизаменуСмо)
      {
        var insuredPersonId = statement.InsuredPerson.Id;
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        var oldStatement =
          session.QueryOver<Statement>()
                 .Where(x => x.IsActive)
                 .And(x => x.Id != statement.Id)
                 .And(x => x.Status.Id != StatusStatement.Cancelled)
                 .And(x => x.Status.Id != StatusStatement.Declined)
                 .And(x => x.InsuredPerson.Id == insuredPersonId)
                 .SingleOrDefault();
        if (oldStatement == null)
        {
          return;
        }

        var oldSmoId = oldStatement.PointDistributionPolicy.Parent.Id;
        if (statement.PointDistributionPolicy.Parent.Id == oldSmoId)
        {
          throw new FaultPersonAlreadyBelongsToSmoException();
        }
      }
    }

    #endregion
  }
}