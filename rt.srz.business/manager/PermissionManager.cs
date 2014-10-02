// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The PermissionManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using NHibernate;
using NHibernate.Criterion;
using rt.srz.model.srz;
using StructureMap;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The PermissionManager.
  /// </summary>
  public partial class PermissionManager
  {
    /// <summary>
    /// ƒобавление разрешени€
    /// </summary>
    /// <param name="code">
    /// </param>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid AddPermission(int code, string name)
    {
      var p = new Permission();
      p.Code = code;
      p.Name = name;
      SaveOrUpdate(p);
      return p.Id;
    }

    /// <summary>
    /// ”даление разрешени€
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeletePermission(Guid id)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>().Delete(pr => pr.Permission.Id == id);
      Delete(p => p.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// ѕровер€ет есть ли в базе разрешение с указанным ид и кодом
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <param name="code">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool ExistsPermissionCode(Guid permissionId, int code)
    {
      var list = GetBy(p => p.Id != permissionId && p.Code == code);
      return list.Count > 0;
    }

    /// <summary>
    /// ѕолучает список разрешений названи€ которых начинаютс€ с указанного значени€
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Permission> GetPermissionsByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return session.QueryOver<Permission>().WhereRestrictionOn(g => g.Name).IsInsensitiveLike(contains, MatchMode.Anywhere).List();
    }

  }
}