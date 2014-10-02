// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The RoleManager.
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
  ///   The RoleManager.
  /// </summary>
  public partial class RoleManager
  {
    /// <summary>
    /// Добавление роли
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid AddRole(string name)
    {
      var role = new Role();
      role.Code = 0;
      role.Name = name;
      SaveOrUpdate(role);
      return role.Id;
    }

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteRole(Guid id)
    {
      // todo start transaction and commit
      ObjectFactory.GetInstance<IPermissionRoleManager>().Delete(pr => pr.Role.Id == id);
      ObjectFactory.GetInstance<IUserGroupRoleManager>().Delete(ugr => ugr.Role.Id == id);
      Delete(r => r.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return session.QueryOver<Role>().WhereRestrictionOn(g => g.Name).IsInsensitiveLike(contains, MatchMode.Anywhere).List();
    }

  }
}