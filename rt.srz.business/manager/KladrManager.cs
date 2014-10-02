// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The KladrManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Linq;

  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The KladrManager.
  /// </summary>
  public partial class KladrManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="tfom">
    /// The tfom.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetFirstLevelByTfoms(Organisation tfom)
    {
      string okato = string.Format("{0}000000", tfom.Okato.Trim());
      return GetBy(x => x.Ocatd == okato && x.Level == 1).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectID">
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetKLADR(Guid objectID)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return (from kladr in session.Query<Kladr>() where kladr.Id == objectID select kladr).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetKLADRByCode(string code)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return (from kladr in session.Query<Kladr>() where kladr.Code == code select kladr).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentID">
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    public IList<Kladr> GetKLADRs(Guid? parentID, string prefix, KLADRLevel? level)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var query = session.QueryOver<Kladr>().Where(x => x.KLADRPARENT.Id == parentID).WhereRestrictionOn(x=> x.Code).IsLike("%00");
      if (level.HasValue)
      {
        query.Where(x => x.Level == level.GetHashCode());
      }

      // поиск с префиксом
      if (!string.IsNullOrEmpty(prefix))
      {
        query.WhereRestrictionOn(x => x.Name).IsLike(prefix);
      }

      return query.OrderBy(x => x.Name).Asc.List();
    }

    #endregion
  }
}