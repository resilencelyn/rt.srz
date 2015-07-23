// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerCacheBaseT.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ManagerCacheBaseT interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.nhibernate
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;

  using rt.core.model.interfaces;

  #endregion

  /// <summary>
  /// The ManagerCacheBaseT interface.
  /// </summary>
  /// <typeparam name="TClass">
  /// тип базового объекта
  /// </typeparam>
  /// <typeparam name="TKey">
  /// </typeparam>
  public interface IManagerCacheBaseT<TClass, TKey> : IManagerCacheBase
    where TClass : class
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получение всех по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    IList<TClass> GetBy(Expression<Func<TClass, bool>> expression);

    /// <summary>
    /// The get by id.
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    /// <returns>
    /// The <see cref="TClass"/>.
    /// </returns>
    TClass GetById(TKey id);

    /// <summary>
    /// Получение одного по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    TClass Single(Expression<Func<TClass, bool>> expression);

    /// <summary>
    /// Получение одного по условию
    /// </summary>
    /// <param name="expression">
    /// The expression.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    TClass SingleOrDefault(Expression<Func<TClass, bool>> expression);

    /// <summary>
    /// The unproxy.
    /// </summary>
    /// <param name="proxy">
    /// The proxy.
    /// </param>
    /// <returns>
    /// The <see cref="TClass"/>.
    /// </returns>
    TClass Unproxy(TClass proxy);

    #endregion
  }
}