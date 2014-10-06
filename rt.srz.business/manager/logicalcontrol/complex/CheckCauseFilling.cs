// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CheckCauseFilling.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The check cause filling.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.logicalcontrol.complex
{
  #region

  using NHibernate;

  using rt.srz.business.Properties;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol.exceptions.step1;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The check cause filling.
  /// </summary>
  public class CheckCauseFilling : Check
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckCauseFilling"/> class.
    /// </summary>
    /// <param name="sessionFactory">
    /// The session factory.
    /// </param>
    public CheckCauseFilling(ISessionFactory sessionFactory)
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
        return Resource.CaptionCheckCauseFilling;
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
    /// <exception cref="FaultThereAreUnclosedStatementsException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      // Есть ли у него другие открытые заявления
      var count =
        session.QueryOver<Statement>()
               .Where(x => x.InsuredPerson.Id == statement.InsuredPerson.Id)
               .And(x => x.Id != statement.Id)
               .WhereRestrictionOn(x => x.Status.Id)
               .IsIn(
                     new[]
                     {
                       StatusStatement.New, StatusStatement.CheckingTheValidity, StatusStatement.Enforceable, 
                       StatusStatement.Performed
                     })
               .RowCount();
      if (count > 0)
      {
        throw new FaultThereAreUnclosedStatementsException();
      }

      //// Проверка по текущей страховке.
      // PeriodInsurance period = null;
      // var query =
      // session.QueryOver<MedicalInsurance>()
      // .JoinAlias(x => x.PeriodInsurances, () => period)
      // .Where(x => period.InsuredPerson.Id == statement.InsuredPerson.Id);
    }

    #endregion
  }
}