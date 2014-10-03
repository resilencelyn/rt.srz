// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwinManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Criterion;

  using NLog;

  using rt.core.business.security.interfaces;
  using rt.core.model.dto;
  using rt.srz.business.manager.cache;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;

  using StructureMap;

  /// <summary>
  ///   The TwinManager.
  /// </summary>
  public partial class TwinManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Аннулирование дубликата
    /// </summary>
    /// <param name="twinId">
    /// The twin Id.
    /// </param>
    public void AnnulateTwin(Guid twinId)
    {
      // ставим у дубликата статус не подтверждён
      var twin = GetById(twinId);
      twin.TwinType = ObjectFactory.GetInstance<IConceptCacheManager>().Single(x => x.Id == TypeTwin.TypeTwin1);
      SaveOrUpdate(twin);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Удаляет все дубликаты которые были рассчитаны только по этому ключу
    /// </summary>
    /// <param name="keyId">
    /// The key Id.
    /// </param>
    public void DeleteTwinsCalculatedOnlyByGivenKey(Guid keyId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var transaction = session.BeginTransaction();
      try
      {
        var sql = @"
delete from TwinsKey
where TwinId in ( 
          select t.RowId 
          from Twins t  
          inner join TwinsKey tk1 on tk1.TwinId = t.RowId
          where tk1.KeyTypeId = @key
          and not exists (select 1 from TwinsKey tk where tk.TwinId = t.RowId and tk.KeyTypeId != @key ))";

        session.CreateSQLQuery(sql).SetParameter("key", keyId).UniqueResult();

        sql = @"
delete from Twins
where RowId in ( 
          select t.RowId 
          from Twins t  
          inner join TwinsKey tk1 on tk1.TwinId = t.RowId
          where tk1.KeyTypeId = @key
          and not exists (select 1 from TwinsKey tk where tk.TwinId = t.RowId and tk.KeyTypeId != @key ))";

        session.CreateSQLQuery(sql).SetParameter("key", keyId).UniqueResult();

        transaction.Commit();
      }
      catch (Exception ex)
      {
        LogManager.GetCurrentClassLogger().Error(ex.Message, ex);
        transaction.Rollback();
        throw;
      }
    }

    /// <summary>
    ///   Получает все дубликаты
    /// </summary>
    /// <returns>
    ///   The <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<Twin> GetTwins()
    {
      // зачитываем все дубликаты со статусом - Кандидат в дубликаты
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      Concept t = null;
      InsuredPerson insuredPerson1 = null;
      MedicalInsurance medicalInsurance1 = null;
      Organisation smo1 = null;

      InsuredPerson insuredPerson2 = null;
      MedicalInsurance medicalInsurance2 = null;
      Organisation smo2 = null;

      var query = session.QueryOver<Twin>().JoinAlias(x => x.TwinType, () => t).Where(x => t.Id == TypeTwin.TypeTwin2);

      if (currentUser.HasTf())
      {
        query.JoinAlias(x => x.FirstInsuredPerson, () => insuredPerson1)
             .JoinAlias(() => insuredPerson1.MedicalInsurances, () => medicalInsurance1)
             .JoinAlias(() => medicalInsurance1.Smo, () => smo1)
             .And(() => medicalInsurance1.IsActive)
             .And(() => smo1.Parent.Id == currentUser.GetTf().Id)
             .JoinAlias(x => x.SecondInsuredPerson, () => insuredPerson2)
             .JoinAlias(() => insuredPerson2.MedicalInsurances, () => medicalInsurance2)
             .JoinAlias(() => medicalInsurance2.Smo, () => smo2)
             .And(() => medicalInsurance2.IsActive)
             .And(() => smo2.Parent.Id == currentUser.GetTf().Id);
      }

      return query.List();
    }

    /// <summary>
    /// Дубликаты по критерию для разбивки постранично
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The <see>
    ///     <cref>SearchResult</cref>
    ///   </see>
    ///   .
    /// </returns>
    public SearchResult<Twin> GetTwins(SearchTwinCriteria criteria)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      Concept twinType = null;
      InsuredPerson insuredPerson1 = null;
      MedicalInsurance medicalInsurance1 = null;
      Organisation smo1 = null;

      InsuredPerson insuredPerson2 = null;
      MedicalInsurance medicalInsurance2 = null;
      Organisation smo2 = null;

      var query =
        session.QueryOver<Twin>()
               .JoinAlias(x => x.TwinType, () => twinType)
               .Where(x => twinType.Id == TypeTwin.TypeTwin2);

      if (currentUser.HasTf())
      {
        var tf = currentUser.GetTf();
        query.JoinAlias(x => x.FirstInsuredPerson, () => insuredPerson1)
             .JoinAlias(() => insuredPerson1.MedicalInsurances, () => medicalInsurance1)
             .JoinAlias(() => medicalInsurance1.Smo, () => smo1)
             .And(() => medicalInsurance1.IsActive)
             .And(() => smo1.Parent.Id == tf.Id)
             .JoinAlias(x => x.SecondInsuredPerson, () => insuredPerson2)
             .JoinAlias(() => insuredPerson2.MedicalInsurances, () => medicalInsurance2)
             .JoinAlias(() => medicalInsurance2.Smo, () => smo2)
             .And(() => medicalInsurance2.IsActive)
             .And(() => smo2.Parent.Id == tf.Id);
      }

      TwinsKey twinKeys = null;
      SearchKeyType skt = null;
      switch (criteria.KeyType)
      {
        case TwinKeyType.All:
          break;
        case TwinKeyType.Standard:
          query.JoinAlias(t => t.TwinsKeys, () => twinKeys)
               .JoinAlias(t => twinKeys.KeyType, () => skt)
               .WhereRestrictionOn(t => skt.Tfoms)
               .IsNull();
          break;
        case TwinKeyType.NonStandard:
          query.JoinAlias(t => t.TwinsKeys, () => twinKeys).Where(t => twinKeys.KeyType.Id == criteria.KeyId);
          break;
      }

      var count = query.RowCount();
      var searchResult = new SearchResult<Twin> { Skip = criteria.Skip, Total = count };
      query.Skip(criteria.Skip).Take(criteria.Take);
      searchResult.Rows = query.List();
      return searchResult;
    }

    /// <summary>
    /// Объединяет дубликаты
    /// </summary>
    /// <param name="twinId">
    /// The twin Id.
    /// </param>
    /// <param name="mainInsuredPersonId">
    /// The main Insured Person Id.
    /// </param>
    /// <param name="secondInsuredPersonId">
    /// The second Insured Person Id.
    /// </param>
    public void JoinTwins(Guid twinId, Guid mainInsuredPersonId, Guid secondInsuredPersonId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var twinManager = ObjectFactory.GetInstance<ITwinManager>();
      var personManager = ObjectFactory.GetInstance<IInsuredPersonManager>();
      var statementManager = ObjectFactory.GetInstance<IStatementManager>();
      var histManager = ObjectFactory.GetInstance<IEmploymentHistoryManager>();
      var searchKeyManager = ObjectFactory.GetInstance<ISearchKeyManager>();
      var medicalInsuranceManager = ObjectFactory.GetInstance<MedicalInsuranceManager>();
      var transaction = session.BeginTransaction();
      try
      {
        var mainInsuredPerson = personManager.GetById(mainInsuredPersonId);
        var secondInsuredPerson = personManager.GetById(secondInsuredPersonId);

        // Перебиваем ссылки при объединении
        // Statement
        var statementList = statementManager.GetBy(x => x.InsuredPerson.Id == secondInsuredPerson.Id);
        foreach (var statement in statementList)
        {
          statement.InsuredPerson = mainInsuredPerson;
          session.Update(statement);
        }

        // SearchKey
        var searchKeys = searchKeyManager.GetBy(x => x.InsuredPerson.Id == secondInsuredPersonId);
        foreach (var searchKey in searchKeys)
        {
          searchKey.InsuredPerson = mainInsuredPerson;
        }

        // EmpoloymentHistory 
        var histories = histManager.GetBy(x => x.InsuredPerson.Id == secondInsuredPersonId);
        foreach (var history in histories)
        {
          history.InsuredPerson = mainInsuredPerson;
          session.Update(history);
        }

        // PeriodInsurances
        var periodInsurances = medicalInsuranceManager.GetBy(x => x.InsuredPerson.Id == secondInsuredPersonId);
        foreach (var periodInsurance in periodInsurances)
        {
          periodInsurance.InsuredPerson = mainInsuredPerson;
          session.Update(periodInsurance);
        }

        // Перестраиваем очередность страховок

        // Перестраиваем заявления
        statementManager.ApplyActive(mainInsuredPerson);

        // Помечаем пипла аннулированным
        secondInsuredPerson.Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusPerson.Annuled);
        session.SaveOrUpdate(secondInsuredPerson);

        // Перебиваем двойников на нового объединенного пипла:
        var twins = GetBy(x => x.FirstInsuredPerson.Id == secondInsuredPerson.Id);
        foreach (var t in twins)
        {
          t.FirstInsuredPerson = mainInsuredPerson;
          session.SaveOrUpdate(t);
        }

        twins = GetBy(x => x.SecondInsuredPerson.Id == secondInsuredPerson.Id);
        foreach (var t in twins)
        {
          t.SecondInsuredPerson = mainInsuredPerson;
          session.SaveOrUpdate(t);
        }

        // проставляем статус - Дубликат отработан
        var twin = twinManager.GetById(twinId);
        twin.TwinType = ObjectFactory.GetInstance<IConceptCacheManager>().Single(x => x.Id == TypeTwin.TypeTwin3);
        twinManager.SaveOrUpdate(twin);

        session.Flush();
        transaction.Commit();
      }
      catch
      {
        transaction.Rollback();
        throw;
      }
    }

    /// <summary>
    /// Помечает дубликат как удаленный
    /// </summary>
    /// <param name="id">
    /// The Id.
    /// </param>
    public void RemoveTwin(Guid id)
    {
      // проставляем статус - Дубликат не подтверждён
      var twin = GetById(id);
      twin.TwinType = ObjectFactory.GetInstance<IConceptCacheManager>().Single(x => x.Id == TypeTwin.TypeTwin1);
      SaveOrUpdate(twin);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Разделение
    /// </summary>
    /// <param name="personId">
    /// The person Id.
    /// </param>
    /// <param name="statementsToSeparate">
    /// The statements To Separate.
    /// </param>
    /// <param name="copyDeadInfo">
    /// The copy Dead Info.
    /// </param>
    /// <param name="status">
    /// The status.
    /// </param>
    public void Separate(Guid personId, IList<Statement> statementsToSeparate, bool copyDeadInfo, int status)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var conceptManager = ObjectFactory.GetInstance<IConceptCacheManager>();
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      var personManager = ObjectFactory.GetInstance<IInsuredPersonManager>();
      var statementManager = ObjectFactory.GetInstance<IStatementManager>();
      var twinManager = ObjectFactory.GetInstance<ITwinManager>();
      var numberPolicyCounterManager = ObjectFactory.GetInstance<INumberPolicyCounterManager>();

      var transaction = session.BeginTransaction();
      try
      {
        var person = personManager.GetById(personId);
        var statementToSeparateIdList = statementsToSeparate.Select(x => x.Id).ToList();

        var searchKeys =
          session.QueryOver<SearchKey>().WhereRestrictionOn(x => x.Statement.Id).IsIn(statementToSeparateIdList).List();

        // Создаем пипла
        var personNew = new InsuredPerson { Status = conceptManager.GetById(status) };

        // Определяем ЕНП, последний, заполненный
        var enp =
          statementsToSeparate.Where(x => !string.IsNullOrEmpty(x.NumberPolicy))
                              .OrderByDescending(x => x.DateFiling)
                              .Select(x => x.NumberPolicy)
                              .FirstOrDefault();
        if (enp == null)
        {
          var statement = statementsToSeparate.OrderByDescending(x => x.DateFiling).FirstOrDefault();
          if (statement != null)
          {
            var personData = statement.InsuredPersonData;

            if (personData.Birthday != null)
            {
              enp = numberPolicyCounterManager.GetNextEnpNumber(
                                                                currentUser.GetTf().Id, 
                                                                personData.Gender.Id, 
                                                                personData.Birthday.Value);
            }
          }
        }

        personNew.MainPolisNumber = enp;

        // создаём новую персону и делаем копию инфы о смерти
        if (copyDeadInfo && person.DeadInfo != null)
        {
          var resultDeadInfo = new DeadInfo
                               {
                                 ActRecordDate = person.DeadInfo.ActRecordDate,
                                 ActRecordNumber = person.DeadInfo.ActRecordNumber,
                                 DateDead = person.DeadInfo.DateDead
                               };
          session.Save(resultDeadInfo);
          personNew.DeadInfo = resultDeadInfo;
        }

        session.Save(personNew);

        // создаём копию истории и назначаем новой персоне    
        var historyManager = ObjectFactory.GetInstance<IEmploymentHistoryManager>();
        var histories = historyManager.GetByInsuredPersonId(person.Id);
        foreach (var history in histories)
        {
          session.Evict(history);
          history.Id = Guid.Empty;
          history.InsuredPerson = personNew;
          session.Save(history);
        }

        // переставляем ссылки в периодах страхования
        MedicalInsurance medicalInsurance = null;

        var periodInsurances =
          session.QueryOver<MedicalInsurance>()
                 .WhereRestrictionOn(x => medicalInsurance.Statement.Id)
                 .IsIn(statementToSeparateIdList)
                 .List();
        foreach (var periodInsurance in periodInsurances)
        {
          periodInsurance.InsuredPerson = personNew;
          session.Update(periodInsurance);
        }

        // переставляем ссылки в ключах поиска
        foreach (var key in searchKeys)
        {
          key.InsuredPerson = personNew;
          session.SaveOrUpdate(key);
        }

        // В заявлениях на разделение переставляем ссылки на новую персону
        foreach (var statement in statementsToSeparate)
        {
          statement.InsuredPerson = personNew;
          session.SaveOrUpdate(statement);
        }

        // Перестраиваем заявления
        statementManager.ApplyActive(person);
        statementManager.ApplyActive(personNew);

        // Отменять обработку дубликата не нужно!!!
        // Ищем все дуликаты с исходным пиплом и пораждаем новые дубликаты этих же дубликатов, но уже с новым пиплом 
        var twins = twinManager.GetBy(x => x.FirstInsuredPerson.Id == personId);
        foreach (var twin in twins)
        {
          session.Evict(twin);
          twin.Id = Guid.Empty;
          twin.FirstInsuredPerson = personNew;
          session.Save(twin);
        }

        twins = twinManager.GetBy(x => x.SecondInsuredPerson.Id == personId);
        foreach (var twin in twins)
        {
          session.Evict(twin);
          twin.Id = Guid.Empty;
          twin.SecondInsuredPerson = personNew;
          session.Save(twin);
        }

        // А теперь помечаем пару как обработанный дубликат
        var twin1 = new Twin
                    {
                      FirstInsuredPerson = person, 
                      SecondInsuredPerson = personNew, 
                      TwinType = conceptManager.GetById(TypeTwin.TypeTwin3)
                    };
        session.Save(twin1);

        session.Flush();
        transaction.Commit();
      }
      catch
      {
        transaction.Dispose();
        throw;
      }
    }

    #endregion
  }
}