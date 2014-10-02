// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoCompleteManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The AutoCompleteManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Linq;
  using System.Collections.Generic;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.model.dto;
  using rt.srz.model.srz;

  using StructureMap;
  using rt.srz.model.dto;
  using rt.core.model;
  using System.Linq.Expressions;
  using rt.core.model.dto.enumerations;

  #endregion

  /// <summary>
  ///   The AutoCompleteManager.
  /// </summary>
  public partial class AutoCompleteManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает список вариантов для имени
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<AutoComplete> GetFirstNameAutoComplete(string prefix)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var query =
        session.QueryOver<AutoComplete>()
          .Where(x => x.Type.Id == model.srz.concepts.AutoComplete.FirstName)
          .WhereRestrictionOn(x => x.Name)
          .IsLike(prefix + "%")
          .OrderBy(x => x.Name)
          .Asc;
      return query.List();
    }

    /// <summary>
    /// Возвращает список вариантов для отчества
    /// </summary>
    /// <param name="prefix">
    /// </param>
    /// <param name="nameId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<AutoComplete> GetMiddleNameAutoComplete(string prefix, Guid nameId)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var middleNameQuery =
        session.QueryOver<AutoComplete>()
          .Where(x => x.Type.Id == model.srz.concepts.AutoComplete.MiddleName)
          .WhereRestrictionOn(x => x.Name)
          .IsLike(prefix + "%");

      // Фильтрация по имени
      if (nameId != Guid.Empty)
      {
        var fistNameSubqury = QueryOver.Of<AutoComplete>().Where(x => x.Id == nameId).Select(x => x.Gender.Id);
        middleNameQuery.WithSubquery.WhereProperty(x => x.Gender.Id).In(fistNameSubqury);
      }

      return middleNameQuery.OrderBy(x => x.Name).Asc.List();
    }

    /// <summary>
    /// Получает результат по критерию для имён и отчеств
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/> . 
    /// </returns>
    public SearchResult<AutoComplete> GetFirstMiddleNames(SearchAutoCompleteCriteria criteria)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      AutoComplete ac = null;
      Concept gender = null;
      Concept type = null;
      var query = session.QueryOver(() => ac).JoinAlias(x => x.Gender, () => gender).JoinAlias(x => x.Type, () => type);
      if (!string.IsNullOrEmpty(criteria.Name))
      {
        query.WhereRestrictionOn(x => x.Name).IsInsensitiveLike(criteria.Name, MatchMode.Anywhere);
      }

      var count = query.RowCount();
      var result = new SearchResult<AutoComplete> { Skip = criteria.Skip, Total = count };

      query = AddOrder(criteria, ac, gender, type, query);
      query.Skip(criteria.Skip).Take(criteria.Take);

      result.Rows = query.List();
      return result;
    }

    /// <summary>
    /// The add order.
    /// </summary>
    /// <param name="criteria">
    /// The criteria. 
    /// </param>
    /// <param name="ac">
    /// The ac. 
    /// </param>
    /// <param name="gender">
    /// The gender. 
    /// </param>
    /// <param name="type">
    /// The type. 
    /// </param>
    /// <param name="query">
    /// The query. 
    /// </param>
    /// <returns>
    /// The <see cref="IQueryOver"/> . 
    /// </returns>
    private IQueryOver<AutoComplete, AutoComplete> AddOrder(
      SearchAutoCompleteCriteria criteria,
      AutoComplete ac,
      Concept gender,
      Concept type,
      IQueryOver<AutoComplete, AutoComplete> query)
    {
      // Сортировка
      if (!string.IsNullOrEmpty(criteria.SortExpression))
      {
        Expression<Func<object>> expression = () => ac.Name;
        switch (criteria.SortExpression)
        {
          case "Name":
            expression = () => ac.Name;
            break;
          case "Gender":
            expression = () => gender.Name;
            break;
          case "Type":
            expression = () => type.Name;
            break;
        }

        query = criteria.SortDirection == SortDirection.Ascending
                  ? query.OrderBy(expression).Asc
                  : query.OrderBy(expression).Desc;
      }

      return query;
    }

    /// <summary>
    /// Проверяет существует ли уже запись в базе с таким же именем, типом, полом
    /// </summary>
    /// <param name="firstMiddleName">
    /// The first Middle Name. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool FirstMiddleNameExists(AutoComplete firstMiddleName)
    {
      return ObjectFactory.GetInstance<IAutoCompleteManager>().GetBy(
          x =>
          x.Id != firstMiddleName.Id && x.Name == firstMiddleName.Name && x.Gender == firstMiddleName.Gender
          && x.Type == firstMiddleName.Type).Count() > 0;
    }

    #endregion
  }
}