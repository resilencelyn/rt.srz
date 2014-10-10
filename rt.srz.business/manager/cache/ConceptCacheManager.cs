// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConceptCacheManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The concept cache manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Linq.Expressions;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The concept cache manager.
  /// </summary>
  public class ConceptCacheManager : ManagerCacheBaseT<Concept, int>, IConceptCacheManager
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ConceptCacheManager"/> class.
    /// </summary>
    /// <param name="repository">
    /// The repository.
    /// </param>
    public ConceptCacheManager(IConceptManager repository)
      : base(repository)
    {
      TimeSpan = new TimeSpan(0, 0, 30, 0);
    }

    #endregion

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
    public override IList<Concept> GetBy(Expression<Func<Concept, bool>> expression)
    {
      if (Cache == null || (DateTime.Now - TimeQueryDb) > TimeSpan)
      {
        Cache =
          Repository.GetAll(int.MaxValue).OrderBy(x => x.Oid.Id).ThenBy(x => x.Relevance).ThenBy(x => x.Name).ToList();
        TimeQueryDb = DateTime.Now;
      }

      return Cache.Where(expression.Compile()).ToList();
    }

    /// <summary>
    /// Получение всех по условию
    /// </summary>
    /// <param name="oids">
    /// The oids.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    public IList<Concept> GetBy(IEnumerable<string> oids)
    {
      // Проверяем время и если истекло грузим из базы
      if ((DateTime.Now - TimeQueryDb) > TimeSpan || Cache == null)
      {
        Cache =
          Repository.GetAll(int.MaxValue).OrderBy(x => x.Oid.Id).ThenBy(x => x.Relevance).ThenBy(x => x.Name).ToList();
        TimeQueryDb = DateTime.Now;
      }

      // Отбираем вишинки
      return Cache.Where(x => oids.Contains(x.Oid.Id)).ToList();
    }

    /// <summary>
    /// Получает список concepts по оид
    /// </summary>
    /// <param name="oidId">
    /// The oid Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/> .
    /// </returns>
    public IList<Concept> GetConceptsByOid(string oidId)
    {
      return GetBy(x => x.Oid.Id == oidId).OrderBy(x => x.Code).ThenBy(x => x.Name).ToList();
    }

    #endregion
  }
}