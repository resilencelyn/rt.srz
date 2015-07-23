// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConceptCacheManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ConceptCacheManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  #region references

  using System.Collections.Generic;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The ConceptCacheManager interface.
  /// </summary>
  public interface IConceptCacheManager : IManagerCacheBaseT<Concept, int>
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получение всех по условию
    /// </summary>
    /// <param name="oids">
    /// The oids.
    /// </param>
    /// <returns>
    /// список удовлетворяющих условию
    /// </returns>
    IList<Concept> GetBy(IEnumerable<string> oids);

    /// <summary>
    /// Получает список concepts по оид
    /// </summary>
    /// <param name="oidId">
    /// The oid Id.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Concept}"/> .
    /// </returns>
    IList<Concept> GetConceptsByOid(string oidId);

    #endregion
  }
}