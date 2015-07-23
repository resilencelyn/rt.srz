// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerBase.cs" company="јль€нс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ManagerBase interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  using NHibernate;
  using NHibernate.Criterion;

  #endregion

  /// <summary>
  /// The ManagerBase interface.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  /// <typeparam name="TKey">
  /// </typeparam>
  public interface IManagerBase<T, TKey> : IDisposable
  {
    // Get Methods
    #region Public Properties

    /// <summary>
    ///   Gets the session.
    /// </summary>
    INHibernateSession Session { get; }

    /// <summary>
    ///   Gets the type.
    /// </summary>
    Type Type { get; }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The any.
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool Any(Expression<Func<T, bool>> expression);

    /// <summary>
    ///   The create criteria.
    /// </summary>
    /// <returns>
    ///   The <see cref="ICriteria" />.
    /// </returns>
    ICriteria CreateCriteria();

    /// <summary>
    /// The delete.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    void Delete(T entity);

    /// <summary>
    /// ”даление по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    void Delete(Expression<Func<T, bool>> expression);

    /// <summary>
    /// The evict.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    void Evict(T entity);

    /// <summary>
    ///   The get all.
    /// </summary>
    /// <returns>
    ///   The <see cref="IList" />.
    /// </returns>
    IList<T> GetAll();

    /// <summary>
    /// The get all.
    /// </summary>
    /// <param name="maxResults">
    /// The max results.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetAll(int maxResults);

    /// <summary>
    /// ѕолучение всех по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетвор€ющих условию
    /// </returns>
    IList<T> GetBy(Expression<Func<T, bool>> expression);

    /// <summary>
    /// The get by criteria.
    /// </summary>
    /// <param name="criterionList">
    /// The criterion list.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetByCriteria(params ICriterion[] criterionList);

    /// <summary>
    /// The get by criteria.
    /// </summary>
    /// <param name="maxResults">
    /// The max results.
    /// </param>
    /// <param name="criterionList">
    /// The criterion list.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetByCriteria(int maxResults, params ICriterion[] criterionList);

    /// <summary>
    /// The get by example.
    /// </summary>
    /// <param name="exampleObject">
    /// The example object.
    /// </param>
    /// <param name="excludePropertyList">
    /// The exclude property list.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetByExample(T exampleObject, params string[] excludePropertyList);

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="Id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    T GetById(TKey Id);

    /// <summary>
    /// The get by query.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetByQuery(string query);

    /// <summary>
    /// The get by query.
    /// </summary>
    /// <param name="maxResults">
    /// The max results.
    /// </param>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    IList<T> GetByQuery(int maxResults, string query);

    /// <summary>
    /// The get unique by criteria.
    /// </summary>
    /// <param name="criterionList">
    /// The criterion list.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    T GetUniqueByCriteria(params ICriterion[] criterionList);

    /// <summary>
    /// The get unique by query.
    /// </summary>
    /// <param name="query">
    /// The query.
    /// </param>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    T GetUniqueByQuery(string query);

    /// <summary>
    /// The refresh.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    void Refresh(T entity);

    // Misc Methods

    // CRUD Methods
    /// <summary>
    /// The save.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    object Save(T entity);

    /// <summary>
    /// The save or update.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    void SaveOrUpdate(T entity);

    /// <summary>
    /// The set fetch mode.
    /// </summary>
    /// <param name="associationPath">
    /// The association path.
    /// </param>
    /// <param name="mode">
    /// The mode.
    /// </param>
    void SetFetchMode(string associationPath, FetchMode mode);

    /// <summary>
    /// ѕолучение одного по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетвор€ющих условию
    /// </returns>
    T SingleOrDefault(Expression<Func<T, bool>> expression);

    /// <summary>
    /// The update.
    /// </summary>
    /// <param name="entity">
    /// The entity.
    /// </param>
    void Update(T entity);

    #endregion
  }
}