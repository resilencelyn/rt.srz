// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementSearchManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The statement search manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;
  using NHibernate;
  using NHibernate.Criterion;
  using NHibernate.Exceptions;
  using NHibernate.SqlCommand;
  using NHibernate.Transform;
  using rt.core.business.security.interfaces;
  using rt.core.model.dto;
  using rt.srz.business.manager.cache;
  using rt.srz.model.algorithms;
  using rt.srz.model.dto;
  using rt.srz.model.enumerations;
  using rt.srz.model.logicalcontrol;
  using rt.srz.model.logicalcontrol.exceptions;
  using rt.srz.model.srz;
  using rt.srz.model.srz.concepts;
  using StructureMap;
  using rt.core.model;
  using rt.core.model.dto.enumerations;

  #endregion

  /// <summary>
  ///   The statement search manager.
  /// </summary>
  public class StatementSearchManager : IStatementSearchManager
  {
    #region Delegates

    /// <summary>
    ///   The add criteria delegate.
    /// </summary>
    /// <param name="criteria">
    ///   The criteria.
    /// </param>
    /// <param name="deatachQuery">
    ///   The deatach query.
    /// </param>
    /// <param name="dpersonDatum">
    ///   The dperson datum.
    /// </param>
    /// <param name="ddocument">
    ///   The ddocument.
    /// </param>
    protected delegate void AddCriteriaDelegate(
      SearchStatementCriteria criteria,
      QueryOver<Statement, Statement> deatachQuery,
      InsuredPersonDatum dpersonDatum,
      Document ddocument);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get insured person by statement.
    /// </summary>
    /// <param name="statement">
    /// The statement.
    /// </param>
    /// <returns>
    /// The <see cref="InsuredPerson"/>.
    /// </returns>
    public InsuredPerson GetInsuredPersonByStatement(Statement statement, IEnumerable<SearchKey> keys)
    {
      var numberPolicyCounterManager = ObjectFactory.GetInstance<INumberPolicyCounterManager>();
      var insuredPersons = GetInsuredPersonsByKeys(keys);

      if (insuredPersons.Count > 0)
      {
        if (insuredPersons.Count > 1)
        {
          //// Заносим двойников в твинсы
          ObjectFactory.GetInstance<IInsuredPersonManager>().AddTwinsFirstAndOther(insuredPersons);
        }

        return insuredPersons.First();
      }

      // Новое застрахованное лицо
      var insuredPersonByStatement = new InsuredPerson
                                       {
                                         Status = ObjectFactory.GetInstance<IConceptCacheManager>().GetById(StatusPerson.Active),
                                       };

      if (!string.IsNullOrEmpty(statement.NumberPolicy))
      {
        insuredPersonByStatement.MainPolisNumber = statement.NumberPolicy;
      }
      else
      {
        var insuredPersonDatum = statement.InsuredPersonData;
        insuredPersonByStatement.MainPolisNumber = numberPolicyCounterManager.GetNextEnpNumber(statement.PointDistributionPolicy.Parent.Parent.Id, insuredPersonDatum.Gender.Id, insuredPersonDatum.Birthday.Value);
      }

      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      session.Save(insuredPersonByStatement);

      return insuredPersonByStatement;
    }

    /// <summary>
    /// Осуществляет поиск заявлений по заданному критерию
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public SearchResult<SearchStatementResult> Search(SearchStatementCriteria criteria)
    {
      SearchResult<SearchStatementResult> searchResult = null;

      // поиск по ключам
      try
      {
        searchResult = Search(criteria, AddCriteriaKeys);
      }
      catch (GenericADOException ex)
      {

      }

      // поиск по вхождению  
      if (searchResult == null || !searchResult.Rows.Any())
      {
        try
        {
          searchResult = Search(criteria, AddCriteriaData);
        }
        catch (GenericADOException ex)
        {
          throw new SearchTimeoutException("Превышен интервал ожидания поиска", ex);
        }
      }

      return searchResult;
    }

    #endregion

    #region Methods

    /// <summary>
    /// The add criteria data.
    /// </summary>
    /// <param name="criteria">
    ///   The criteria.
    /// </param>
    /// <param name="deatachQuery">
    ///   The deatach query.
    /// </param>
    /// <param name="dpersonDatum">
    ///   The dperson datum.
    /// </param>
    /// <param name="ddocument">
    ///   The ddocument.
    /// </param>
    /// <param name="dmedicalInsurance"></param>
    /// <param name="emptyCriteria"></param>
    private void AddCriteriaData(SearchStatementCriteria criteria, QueryOver<Statement, Statement> deatachQuery, InsuredPersonDatum dpersonDatum, Document ddocument, MedicalInsurance dmedicalInsurance, bool emptyCriteria)
    {
      // Номер ВС
      if (!string.IsNullOrEmpty(criteria.CertificateNumber))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dmedicalInsurance.PolisNumber == criteria.CertificateNumber).And(x => dmedicalInsurance.PolisType.Id == PolisType.В);
      }

      // Имя
      if (!string.IsNullOrEmpty(criteria.FirstName))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.FirstName == criteria.FirstName.Trim());
      }

      // Фамилия
      if (!string.IsNullOrEmpty(criteria.LastName))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.LastName== criteria.LastName.Trim());
      }

      // Отчество
      if (!string.IsNullOrEmpty(criteria.MiddleName))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.MiddleName == criteria.MiddleName.Trim());
      }

      // СНИЛС
      if (!string.IsNullOrEmpty(criteria.SNILS))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.Snils == SnilsChecker.SsToShort(criteria.SNILS));
      }

      // Тип документа
      if (criteria.DocumentTypeId > 0 && (!string.IsNullOrEmpty(criteria.DocumentSeries) || !string.IsNullOrEmpty(criteria.DocumentNumber)))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => ddocument.DocumentType.Id == criteria.DocumentTypeId);
      }

      // Серия документа
      if (!string.IsNullOrEmpty(criteria.DocumentSeries))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => ddocument.Series == criteria.DocumentSeries);
      }

      // Номер документа
      if (!string.IsNullOrEmpty(criteria.DocumentNumber))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => ddocument.Number == criteria.DocumentNumber);
      }

      // Номер документа
      if (!string.IsNullOrEmpty(criteria.BirthPlace))
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.Birthplace == criteria.BirthPlace.Trim());
      }

      // Номер полиса
      if (!string.IsNullOrEmpty(criteria.PolicyNumber))
      {
        emptyCriteria = false;
        //deatachQuery.Where(x => x.NumberPolicy == criteria.PolicyNumber.Trim());
        deatachQuery.Where(x => dmedicalInsurance.Enp == criteria.PolicyNumber.Trim()).And(x => dmedicalInsurance.PolisType.Id != PolisType.В);
      }

      // Дата рождения
      if (criteria.BirthDate.HasValue)
      {
        emptyCriteria = false;
        deatachQuery.Where(x => dpersonDatum.Birthday == criteria.BirthDate.Value);
      }

      if (!string.IsNullOrEmpty(criteria.Error))
      {
        emptyCriteria = false;
        Error error = null;
        deatachQuery.JoinAlias(x => x.Errors, () => error).Where(x => error.Message1 == criteria.Error);
      }

      // если не сработал ни один критерий то осталяем выборку пустой
      if (emptyCriteria)
      {
        throw new SetParameterSearchException();
      }
    }

    /// <summary>
    /// The add criteria keys.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <param name="deatachQuery">
    /// The deatach query.
    /// </param>
    /// <param name="dpersonDatum">
    /// The dperson datum.
    /// </param>
    /// <param name="ddocument">
    /// The ddocument.
    /// </param>
    /// <param name="emptyCriteria">
    /// The empty Criteria.
    /// </param>
    private void AddCriteriaKeys(
      SearchStatementCriteria criteria,
      QueryOver<Statement, Statement> deatachQuery,
      InsuredPersonDatum dpersonDatum,
      Document ddocument,
      MedicalInsurance dmedicalInsurance,
      bool emptyCriteria)
    {
      ////// Статус заявления
      ////if (criteria.StatementStatus > 0)
      ////{
      ////  deatachQuery.Where(x => x.Status.Id == criteria.StatementStatus);
      ////}

      ////// Тип заявления
      ////if (criteria.StatementType > 0)
      ////{
      ////  deatachQuery.WhereRestrictionOn(x => x.CauseFiling.Id)
      ////    .IsInG(Statement.GetCauseFillingByType(criteria.StatementType));
      ////}

      ////// Поиск по дате подачи заявления
      ////if (criteria.UseDateFiling && criteria.DateFilingFrom != null && criteria.DateFilingTo != null)
      ////{
      ////  if (criteria.DateFilingFrom > criteria.DateFilingTo)
      ////  {
      ////    throw new SearchException("Дата начала периода больше, чем дата его окончания.");
      ////  }

      ////  deatachQuery.WhereRestrictionOn(x => x.DateFiling).IsBetween(criteria.DateFilingFrom).And(criteria.DateFilingTo);
      ////}

      var statement = new Statement
                        {
                          InsuredPersonData =
                            new InsuredPersonDatum
                              {
                                Birthday = criteria.BirthDate,
                                Birthplace = criteria.BirthPlace,
                                FirstName = criteria.FirstName,
                                LastName = criteria.LastName,
                                MiddleName = criteria.MiddleName,
                                Snils = criteria.SNILS,
                                NotCheckSnils = criteria.NotCheckSnils
                              },
                          DocumentUdl =
                            new Document
                              {
                                Series = criteria.DocumentSeries,
                                Number = criteria.DocumentNumber,
                                DocumentType =
                                  ObjectFactory.GetInstance<IConceptCacheManager>()
                                  .GetById(criteria.DocumentTypeId)
                              },
                        };

      var searchKeyManager = ObjectFactory.GetInstance<ISearchKeyManager>();

      var keys = searchKeyManager.CalculateStandardKeys(statement);

      var queryKeys =
        QueryOver.Of<SearchKey>()
          .WhereRestrictionOn(x => x.KeyValue)
          .IsIn(keys.Select(y => y.KeyValue).ToList())
          .Select(x => x.InsuredPerson.Id);

      deatachQuery.WithSubquery.WhereProperty(x => x.InsuredPerson.Id).In(queryKeys);
    }

    /// <summary>
    /// The add order.
    /// </summary>
    /// <param name="criteria">
    ///   The criteria.
    /// </param>
    /// <param name="statement">
    ///   The statement.
    /// </param>
    /// <param name="cause"></param>
    /// <param name="smo">
    ///   The smo.
    /// </param>
    /// <param name="personDatum">
    ///   The person datum.
    /// </param>
    /// <param name="gender">
    ///   The gender.
    /// </param>
    /// <param name="citizenship">
    ///   The citizenship.
    /// </param>
    /// <param name="documentType">
    ///   The document type.
    /// </param>
    /// <param name="document">
    ///   The document.
    /// </param>
    /// <param name="dmedicalInsurance"></param>
    /// <param name="query">
    ///   The query.
    /// </param>
    /// <param name="causeReinsurance">
    /// The cause filing.
    /// </param>
    /// <returns>
    /// The <see cref="IQueryOver"/>.
    /// </returns>
    private IQueryOver<Statement, Statement> AddOrder(SearchStatementCriteria criteria, Statement statement, Concept cause, Organisation smo, InsuredPersonDatum personDatum, Concept gender, Concept citizenship, Concept documentType, Document document, MedicalInsurance dmedicalInsurance, IQueryOver<Statement, Statement> query)
    {
      // Сортировка
      if (!string.IsNullOrEmpty(criteria.SortExpression))
      {
        Expression<Func<object>> expression = () => statement.DateFiling;
        switch (criteria.SortExpression)
        {
          case "DateFiling":
            expression = () => statement.DateFiling;
            break;

          case "CauseFilling":
            expression = () => cause.Name;
            break;
          case "SMO":
            expression = () => smo.FullName;
            break;
          case "FirstName":
            expression = () => personDatum.FirstName;
            break;
          case "LastName":
            expression = () => personDatum.LastName;
            break;
          case "MiddleName":
            expression = () => personDatum.MiddleName;
            break;
          case "Gender":
            expression = () => gender.Name;
            break;
          case "Birthday":
            expression = () => personDatum.Birthday;
            break;
          case "Citizenship":
            expression = () => citizenship.Name;
            break;
          case "DocumentType":
            expression = () => documentType.Name;
            break;
          case "DocumentId":
            expression = () => document.Number;
            break;
          case "SNILS":
            expression = () => personDatum.Snils;
            break;
          ////case "NumberTemporaryCertificate":
          ////  expression = () => statement.NumberTemporaryCertificate;
          ////  break;
        }

        query = criteria.SortDirection == SortDirection.Ascending
                  ? query.OrderBy(expression).Asc
                  : query.OrderBy(expression).Desc;


      }

      query = query.OrderBy(x => x.Id).Asc;
      return query;
    }

    /// <summary>
    /// The get insured persons by keys.
    /// </summary>
    /// <param name="keys">
    /// The keys.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<InsuredPerson> GetInsuredPersonsByKeys(IEnumerable<SearchKey> keys)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var insuredPersonManager = ObjectFactory.GetInstance<IInsuredPersonManager>();
      IList<InsuredPerson> insuredPersons = new List<InsuredPerson>();
      var keysInserting = keys.Where(x => x.KeyType.Insertion);

      // поиск по ключам
      if (keys.Any())
      {
        SearchKeyType kt = null;
        var query = session.QueryOver<SearchKey>()
          .JoinAlias(x => x.KeyType, () => kt)
          .Where(x => kt.Insertion)
          .WhereRestrictionOn(x => x.KeyValue).IsIn(keysInserting.Select(y => y.KeyValue).ToList())
          .OrderBy(x => kt.Weight).Asc
          .SelectList(x => x
            .SelectGroup(y => y.InsuredPerson.Id)
            .SelectGroup(y => kt.Weight))
          .List<object[]>();



        insuredPersons = query.GroupBy(x => x[0]).Select(x => insuredPersonManager.GetById((Guid)x.Key)).Where(x=> x != null).ToList();
      }

      return insuredPersons;
    }

    /// <summary>
    /// The search.
    /// </summary>
    /// <param name="criteria">
    /// The criteria.
    /// </param>
    /// <param name="addCriteriaDelegate">
    /// The add criteria delegate.
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    private SearchResult<SearchStatementResult> Search(
      SearchStatementCriteria criteria,
      Action<SearchStatementCriteria, QueryOver<Statement, Statement>, InsuredPersonDatum, Document, MedicalInsurance, bool> addCriteriaDelegate)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();

      InsuredPersonDatum dpersonDatum = null;
      InsuredPerson dperson = null;
      Document ddocument = null;
      MedicalInsurance dmedicalInsurance = null;
      var deatachQuery =
        QueryOver.Of<Statement>()
          .JoinAlias(x => x.InsuredPersonData, () => dpersonDatum)
          .JoinAlias(x => x.InsuredPerson, () => dperson)
          .JoinAlias(x => x.DocumentUdl, () => ddocument)
          .JoinAlias(x => x.MedicalInsurances, () => dmedicalInsurance, JoinType.LeftOuterJoin)
          .Select(x => dperson.Id);

      Statement statement = null;
      InsuredPersonDatum personDatum = null;
      InsuredPerson person = null;
      CauseReinsurance cause = null;
      Organisation tfom = null;
      Organisation smo = null;
      Organisation point = null;
      Concept gender = null;
      Concept citizenship = null;
      Document document = null;
      Concept documentType = null;
      Concept status = null;
      var query =
        session.QueryOver(() => statement)
          .Left.JoinAlias(x => x.Status, () => status)
          .Left.JoinAlias(x => x.InsuredPersonData, () => personDatum)
          .Left.JoinAlias(x => x.InsuredPerson, () => person)
          .Left.JoinAlias(x => x.CauseFiling, () => cause)
          .Left.JoinAlias(x => x.PointDistributionPolicy, () => point)
          .Left.JoinAlias(() => point.Parent, () => smo)
          .Left.JoinAlias(() => smo.Parent, () => tfom)
          .Left.JoinAlias(() => personDatum.Gender, () => gender)
          .Left.JoinAlias(() => personDatum.Citizenship, () => citizenship)
          .Left.JoinAlias(x => x.DocumentUdl, () => document)
          .Left.JoinAlias(() => document.DocumentType, () => documentType)
          .WithSubquery.WhereProperty(x => x.InsuredPerson.Id)
          .In(deatachQuery);

      var emptyCriteria = true;

      // Статус заявления
      if (criteria.StatementStatus > 0)
      {
        switch (criteria.StatementStatus)
        {
          case 9000:
            query.Where(x => document.IsBad);
            query
              .Where(x => point.Parent.Id == ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser().PointDistributionPolicy.Parent.Id);
            break;
          case 9001:
            query.Where(x => personDatum.IsBadSnils);
            query
              .Where(x => point.Parent.Id == ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser().PointDistributionPolicy.Parent.Id);
            break;
          default:
            query.Where(x => x.Status.Id == criteria.StatementStatus);
            break;
        }

        emptyCriteria = false;
      }

      // Тип заявления
      if (criteria.StatementType > 0)
      {
        emptyCriteria = false;
        query.WhereRestrictionOn(x => x.CauseFiling.Id).IsInG(Statement.GetCauseFillingByType(criteria.StatementType));
      }

      // Поиск по дате подачи заявления
      if (criteria.UseDateFiling && criteria.DateFilingFrom != null && criteria.DateFilingTo != null)
      {
        if (criteria.DateFilingFrom > criteria.DateFilingTo)
        {
          throw new SearchException("Дата начала периода больше, чем дата его окончания.");
        }

        criteria.DateFilingTo = criteria.DateFilingTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

        emptyCriteria = false;
        query.WhereRestrictionOn(x => x.DateFiling).IsBetween(criteria.DateFilingFrom).And(criteria.DateFilingTo);
      }

      // Выводить только последние значения
      if (criteria.ReturnLastStatement)
      {
        query.Where(x => x.IsActive);
      }

      addCriteriaDelegate(criteria, deatachQuery, dpersonDatum, ddocument, dmedicalInsurance, emptyCriteria);

      var count = query.RowCount();
      var searchResult = new SearchResult<SearchStatementResult> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, statement, cause, smo, personDatum, gender, citizenship, documentType, document, dmedicalInsurance, query);

      query.Skip(criteria.Skip).Take(criteria.Take);

      SearchStatementResult result = null;
      var res =
        query.SelectList(
          y =>
          y.Select(x => x.Id).WithAlias(() => result.Id)
           .Select(x => x.DateFiling).WithAlias(() => result.DateFiling)
           .Select(x => x.IsActive).WithAlias(() => result.IsActive)
           .Select(x => cause.Name).WithAlias(() => result.CauseFiling)
           .Select(x => x.CauseFiling.Id).WithAlias(() => result.CauseFilingId)
           .Select(x => smo.Id).WithAlias(() => result.SmoId)
           .Select(x => smo.ShortName).WithAlias(() => result.Smo)
           .Select(x => smo.Ogrn).WithAlias(() => result.SmoOGRN)
           .Select(x => tfom.Okato).WithAlias(() => result.TfomOKATO)
           .Select(x => personDatum.FirstName).WithAlias(() => result.FirstName)
           .Select(x => personDatum.LastName).WithAlias(() => result.LastName)
           .Select(x => personDatum.MiddleName).WithAlias(() => result.MiddleName)
           .Select(x => gender.Name).WithAlias(() => result.Gender)
           .Select(x => personDatum.Birthday).WithAlias(() => result.Birthday)
           .Select(x => personDatum.Birthplace).WithAlias(() => result.Birthplace)
           .Select(x => x.Address2).WithAlias(() => result.AddressLive)
           .Select(x => x.Address).WithAlias(() => result.AddressRegistration)
           .Select(x => x.NumberPolicy).WithAlias(() => result.PolicyNumber)
           .Select(x => citizenship.Name).WithAlias(() => result.Citizenship)
           .Select(x => documentType.Name).WithAlias(() => result.DocumentType)
           .Select(x => document.Series).WithAlias(() => result.DocumentSeria)
           .Select(x => document.Number).WithAlias(() => result.DocumentNumber)
           .Select(x => personDatum.Snils).WithAlias(() => result.Snils)
           .Select(x => status.Name).WithAlias(() => result.StatusStatement)
           .Select(x => status.Id).WithAlias(() => result.Status)
           .Select(x => person.Status.Id).WithAlias(() => result.PersonStatus)
           .Select(x => x.IsExportPolis).WithAlias(() => result.IsSinhronized))
          .TransformUsing(Transformers.AliasToBean<SearchStatementResult>())
          .List<SearchStatementResult>();

      var errorManager = ObjectFactory.GetInstance<IErrorManager>();
      var conceptCacheManager = ObjectFactory.GetInstance<IConceptCacheManager>();

      // получение текущего пользователя и текущей страховой
      var currentUser = ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
      Organisation currentSmo = null;
      if (currentUser != null && currentUser.PointDistributionPolicy != null)
      {
        currentSmo = currentUser.PointDistributionPolicy.Parent;
      }

      foreach (var statementResult in res)
      {
        if (statementResult.IsActive)
        {
          statementResult.StatusStatement += " (Активное";
          if (statementResult.PersonStatus == StatusPerson.Dead)
          {
            statementResult.StatusStatement += " , Умерший";
          }
          statementResult.StatusStatement += ")";
        }

        statementResult.Errors = errorManager.GetBy(x => x.Statement.Id == statementResult.Id).Select(x => string.IsNullOrEmpty(x.Repl) ? x.Message1 : string.Format("{0} ({1})", x.Message1, x.Repl)).ToList();
        statementResult.TypeStatement =
          conceptCacheManager.GetById(Statement.GetTypeStatementId(statementResult.CauseFilingId)).Name;
        statementResult.FromCurrentSmo = currentSmo.Id == statementResult.SmoId;
        statementResult.DateInsuranceEnd = new DateTime(2030, 1, 1); //TODO: логика для даты окончания

        var temp = ObjectFactory.GetInstance<IMedicalInsuranceManager>().GetBy(x => x.Statement.Id == statementResult.Id && x.IsActive && x.PolisType.Id == PolisType.В).FirstOrDefault();
        if (temp != null)
          statementResult.NumberTemporaryCertificate = temp.PolisNumber;

        var polis = ObjectFactory.GetInstance<IMedicalInsuranceManager>().GetBy(x => x.Statement.Id == statementResult.Id && x.IsActive && x.PolisType.Id != PolisType.В).FirstOrDefault();
        if (polis != null)
          statementResult.PolicyNumber = polis.Enp;
      }

      searchResult.Rows = res;
      return searchResult;
    }

    #endregion
  }
}