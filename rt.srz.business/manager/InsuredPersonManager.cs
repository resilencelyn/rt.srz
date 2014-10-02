// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsuredPersonManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;

  using rt.srz.business.manager.cache;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The InsuredPersonManager.
  /// </summary>
  public partial class InsuredPersonManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// При приклеплении человека к одной истории, если возникает ситуация, что по другим ключам он принадлежит другому человеку, то заносим первого 
    ///   (так как он будет прикреплен к первому) и все отсальные в двойники, чтобы оператор ТФ уже проверил этого двойника
    /// </summary>
    /// <param name="insuredPersons">
    /// The insured persons. 
    /// </param>
    public void AddTwinsFirstAndOther(IList<InsuredPerson> insuredPersons)
    {
      // В списке должнобыть больше одной записи
      if (insuredPersons.Count <= 1)
      {
        return;
      }

      var twinsManager = ObjectFactory.GetInstance<ITwinManager>();
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      var p1 = insuredPersons.First();
      foreach (var p2 in insuredPersons.Skip(1))
      {
        var p3 = p2;
        if (
          !twinsManager.Any(
            x =>
            ((x.FirstInsuredPerson.Id == p1.Id && x.SecondInsuredPerson.Id == p3.Id)
             || (x.FirstInsuredPerson.Id == p3.Id && x.SecondInsuredPerson.Id == p1.Id))))
        {
          var twin = new Twin
            {
              FirstInsuredPerson = p1,
              SecondInsuredPerson = p2,
              TwinType = conceptManager.GetById(TypeTwin.TypeTwin2)
            };

          session.Save(twin);
        }
      }
    }

    /// <summary>
    /// Входит ли указанная персона в объединение как главное или как второе лицо
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool InsuredInJoined(Guid personId)
    {
      return true;
    }

    /// <summary>
    /// The on canceled or remove statement.
    /// </summary>
    /// <param name="insuredPerson">
    /// The insured person.
    /// </param>
    public void OnCanceledOrRemoveStatement(InsuredPerson insuredPerson)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      if (insuredPerson.Statements != null && (!insuredPerson.Statements.Any() || insuredPerson.Statements.All(x => StatusStatement.IsAnnuled(x.Status.Id))))
      {
        insuredPerson.Status = conceptManager.GetById(StatusPerson.Annuled);
        session.Update(insuredPerson);

        foreach (var period in insuredPerson.MedicalInsurances)
        {
          period.IsActive = false;
          session.Update(period);
        }
      }
    }

    /// <summary>
    /// Удаление инфы о смерти
    /// </summary>
    /// <param name="statementId"></param>
    public void DeleteDeathInfo(Guid statementId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var statement = ObjectFactory.GetInstance<IStatementManager>().GetById(statementId);
      var person = statement.InsuredPerson;
      person.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById((int)StatusPerson.Active);
      SaveOrUpdate(person);
      session.Flush();
    }

    #endregion
  }
}