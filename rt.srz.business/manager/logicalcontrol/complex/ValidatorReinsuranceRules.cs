using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using StructureMap;
using rt.srz.business.Properties;
using rt.srz.model.srz;
using rt.srz.model.srz.concepts;
using rt.srz.model.logicalcontrol.exceptions.step1;

namespace rt.srz.business.manager.logicalcontrol.complex
{
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
    /// Gets the caption.
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
    /// <exception cref="FaultPersonAlreadyBelongsToSmoException">
    /// </exception>
    public override void CheckObject(Statement statement)
    {
      // Пропускаем проверку если причина - "Заявление на выбор или замену СМО не подавалось"
      if (statement.CauseFiling != null && statement.CauseFiling.Id == CauseReinsurance.Initialization)
      {
        return;
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var oldStatementList = session.QueryOver<Statement>()
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
