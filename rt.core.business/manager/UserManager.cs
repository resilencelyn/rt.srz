// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The UserManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.model;
  using rt.core.model.core;

  using StructureMap;

  /// <summary>
  ///   The UserManager.
  /// </summary>
  public partial class UserManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Назначение пункта выдачи пользователю
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    public void AssignPdpToUser(Guid userId, Guid? pdpId)
    {
      var user = GetById(userId);
      user.PointDistributionPolicyId = pdpId;
      SaveOrUpdate(user);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    public void DeleteUser(Guid userId)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().Delete(x => x.User.Id == userId);
      ObjectFactory.GetInstance<IUserGroupManager>().Delete(x => x.User.Id == userId);
      MarkAsDeleted(userId);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Разрешено ли пользователю разрешение с указанным кодом
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="permissionCode">
    /// The permission Code.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool GetIsUserAllowPermission(Guid userId, int permissionCode)
    {
      var user = GetById(userId);
      return InternalGetIsUserAllowPermission(user, permissionCode);
    }

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IsUserAdminSmo(Guid userId)
    {
      var userManager = ObjectFactory.GetInstance<IUserManager>();
      return userManager.GetIsUserAllowPermission(userId, (int)PermissionCode.AttachToOwnSmo);
    }

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool IsUserAdminTf(Guid userId)
    {
      var userManager = ObjectFactory.GetInstance<IUserManager>();
      return userManager.GetIsUserAllowPermission(userId, (int)PermissionCode.AttachToOwnRegion);
    }

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns>
    ///   The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<User> GetUsers()
    {
      return GetBy(x => x.IsApproved);
    }

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// The contains.
    /// </param>
    /// <returns>
    /// The
    ///   <see>
    ///     <cref>IList</cref>
    ///   </see>
    ///   .
    /// </returns>
    public IList<User> GetUsersByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<User>()
               .Where(x => x.IsApproved)
               .WhereRestrictionOn(u => u.Login)
               .IsInsensitiveLike(contains, MatchMode.Anywhere)
               .List();
    }

    /// <summary>
    /// Имеет ли пользователь роль администратора или входит в группы любая из которых имеет роль администратора
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    public bool IsUserHasAdminPermissions(User user)
    {
      return user.IsAdmin
             || user.UserGroups.Select(x => x.Group)
                    .Any(y => y.UserGroupRoles.Select(x => x.Role).Any(role => role.Code == 1));
    }

    /// <summary>
    /// The mark as deleted.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    public void MarkAsDeleted(Guid userId)
    {
      var user = GetById(userId);
      user.IsApproved = false;
      SaveOrUpdate(user);
    }

    #endregion

    #region Methods

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
        if (role.Code == 1)
        {
          return true;
        }

        var item = role.PermissionRoles.FirstOrDefault(x => x.Permission.Code == permissionCode);
        if (item != null)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// The internal get is user allow permission.
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <param name="permissionCode">
    /// The permission code.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
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

    #endregion
  }
}