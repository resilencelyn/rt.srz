// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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
      var okato = string.Format("{0}000000", tfom.Okato.Trim());
      return GetBy(x => x.Ocatd == okato && x.Level == 1).FirstOrDefault();
    }

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    /// The parent Id.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// The level.
    /// </param>
    /// <returns>
    /// The <see cref="IList{Kladr}"/>.
    /// </returns>
    public IList<Kladr> GetKladrs(Guid? parentId, string prefix, KladrLevel? level)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      var query =
        session.QueryOver<Kladr>()
               .Where(x => x.KLADRPARENT.Id == parentId)
               .WhereRestrictionOn(x => x.Code)
               .IsLike("%00");
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