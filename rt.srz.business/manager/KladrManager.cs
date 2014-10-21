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
  using System.Text;

  using NHibernate;

  using rt.core.model.interfaces;
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
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/>.
    /// </returns>
    public IAddress GetFirstLevelByTfoms(string okato)
    {
      return GetBy(x => x.Okato == okato && x.Level == 1).FirstOrDefault();
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

    /// <summary>
    /// The get structure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="StructureAddress"/>.
    /// </returns>
    public StructureAddress GetStructureAddress(Guid objectId)
    {
      var structureAddress = new StructureAddress();
      var kladr = GetById(objectId);
      do
      {
        var strTemp = kladr.Name + " " + kladr.Socr;
        switch (kladr.Level)
        {
          case (int)KladrLevel.Subject:
            {
              structureAddress.Subject = strTemp;
            }

            break;
          case (int)KladrLevel.Area:
            {
              structureAddress.Area = strTemp;
            }

            break;
          case (int)KladrLevel.City:
            {
              structureAddress.City = strTemp;
            }

            break;
          case (int)KladrLevel.Town:
            {
              structureAddress.Town = strTemp;
            }

            break;
          case (int)KladrLevel.Street:
            {
              structureAddress.Street = strTemp;
            }

            break;
        }

        kladr = kladr.ParentId.HasValue ? GetById(kladr.ParentId.Value) : null;
      }
      while (kladr != null);

      return structureAddress;
    }

    /// <summary>
    /// The get unstructure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetUnstructureAddress(Guid objectId)
    {
      var valueBuilder = new StringBuilder();
      var kladr = GetById(objectId);
      do
      {
        valueBuilder.Insert(0, string.Format("," + kladr.Name + " " + kladr.Socr + "."));
        kladr = kladr.KLADRPARENT;
      }
      while (kladr != null);

      if (valueBuilder.Length > 0)
      {
        valueBuilder.Remove(0, 1);
      }

      valueBuilder.Append(",");

      return valueBuilder.ToString();
    }

    /// <summary>
    /// The hierarchy build.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string HierarchyBuild(Guid objectId)
    {
      var hierarchyBuilder = new StringBuilder();
      var kladr = GetById(objectId);
      do
      {
        hierarchyBuilder.Insert(0, string.Format(";" + kladr.Id));
        kladr = kladr.KLADRPARENT;
      }
      while (kladr != null);

      if (hierarchyBuilder.Length > 0)
      {
        hierarchyBuilder.Remove(0, 1);
      }

      return hierarchyBuilder.ToString();
    }

    #endregion
  }
}