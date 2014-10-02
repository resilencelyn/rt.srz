// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The UserManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System.Collections.Generic;

  using NHibernate;

  using StructureMap;

  using rt.srz.model.srz;
  using System;
  using System.Linq;
  using rt.srz.model.enumerations;
  using rt.srz.model.common;
  using rt.core.business.security.interfaces;
  using NHibernate.Criterion;
  
  /// <summary>
  ///   The UserManager.
  /// </summary>
  public partial class UserManager
  {
    public void MarkAsDeleted(Guid userId)
    {
      User user = GetById(userId);
      user.IsApproved = false;
      SaveOrUpdate(user);
    }

    /// <summary>
    /// Назначение пункта выдачи пользователю
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="pdpId">
    /// </param>
    public void AssignPdpToUser(Guid userId, Guid? pdpId)
    {
      var user = GetById(userId);
      if (pdpId == null)
      {
        user.PointDistributionPolicy = null;
      }
      else
      {
        var organisationManager = ObjectFactory.GetInstance<IOrganisationManager>();
        user.PointDistributionPolicy = organisationManager.GetById(pdpId.Value);
      }

      SaveOrUpdate(user);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    public void DeleteUser(Guid userId)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().Delete(x => x.User.Id == userId);
      ObjectFactory.GetInstance<IUserGroupManager>().Delete(x => x.User.Id == userId);
      MarkAsDeleted(userId);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminTF(Guid userId)
    {
      return GetIsUserAllowPermission(userId, (int)PermissionCode.AttachToOwnRegion);
    }

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminSmo(Guid userId)
    {
      return GetIsUserAllowPermission(userId, (int)PermissionCode.AttachToOwnSmo);
    }

    /// <summary>
    /// Разрешено ли пользователю разрешение с указанным кодом
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="permissionCode">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool GetIsUserAllowPermission(Guid userId, int permissionCode)
    {
      var user = GetById(userId);
      return InternalGetIsUserAllowPermission(user, permissionCode);
    }

    private bool InternalGetIsUserAllowPermission(User user, int permissionCode)
    {
      if (user.IsAdmin)
      {
        return true;
      }

      // проверяем разрешение у пользователя 
      var check = CheckRoles(user.UserGroupRoles.Select(x => x.Role), permissionCode);
      if (check)
      {
        return true;
      }

      // проверяем разрешение у всех групп, куда входит пользователь. 
      foreach (var group in user.UserGroups.Select(x => x.Group))
      {
        check = CheckRoles(group.UserGroupRoles.Select(x => x.Role), permissionCode);
        if (check)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// The check roles.
    /// </summary>
    /// <param name="roles">
    /// The roles. 
    /// </param>
    /// <param name="permissionCode">
    /// The permission code. 
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    private bool CheckRoles(IEnumerable<Role> roles, int permissionCode)
    {
      foreach (var role in roles)
      {
        // если роль админа, то разрешаем
        if (role.Code == InfoUtility.C_AdminCode)
        {
          return true;
        }

        var item = role.PermissionRoles.Where(x => x.Permission.Code == permissionCode).FirstOrDefault();
        if (item != null)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsers()
    {
      return GetBy(x => x.IsApproved == true);
    }

    /// <summary>
    /// Имеет ли пользователь роль администратора или входит в группы любая из которых имеет роль администратора
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    public bool IsUserHasAdminPermissions(User user)
    {
      if (user.IsAdmin)
      {
        return true;
      }

      foreach (var group in user.UserGroups.Select(x => x.Group))
      {
        foreach (var role in group.UserGroupRoles.Select(x => x.Role))
        {
          // если роль админа, то разрешаем
          if (role.Code == InfoUtility.C_AdminCode)
          {
            return true;
          }
        }
      }

      return false;
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      var sec = ObjectFactory.GetInstance<ISecurityProvider>();
      var currentUser = sec.GetCurrentUser();
      if (IsUserHasAdminPermissions(currentUser))
      {
        return GetUsers();
      }

      if (currentUser.PointDistributionPolicy == null)
      {
        return null;
      }


        // свой регион
      else if (IsUserAdminTF(currentUser.Id))
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        User user = null;
        Organisation foms = null;
        Organisation smo = null;
        Organisation pdp = null;
        var curFomsId = currentUser.PointDistributionPolicy.Parent.Parent.Id;
        var query =
          session.QueryOver(() => user).JoinAlias(x => x.PointDistributionPolicy, () => pdp).JoinAlias(
            () => pdp.Parent, () => smo).JoinAlias(() => smo.Parent, () => foms).Where(x => foms.Id == curFomsId && user.IsApproved == true);
        return query.List();
      }


        // своя смо
      else if (IsUserAdminSmo(currentUser.Id))
      {
        var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
        User user = null;
        Organisation foms = null;
        Organisation smo = null;
        Organisation pdp = null;
        var curSmoId = currentUser.PointDistributionPolicy.Parent.Id;
        var query =
          session.QueryOver(() => user).JoinAlias(x => x.PointDistributionPolicy, () => pdp).JoinAlias(
            () => pdp.Parent, () => smo).JoinAlias(() => smo.Parent, () => foms).Where(x => smo.Id == curSmoId && user.IsApproved == true);
        return query.List();
      }

      return null;
    }

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<User> GetUsersByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return session.QueryOver<User>().Where(x => x.IsApproved == true).WhereRestrictionOn(u => u.Login).IsInsensitiveLike(contains, MatchMode.Anywhere).List();
    }


  }
}