// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The security service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Security
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.business.manager;
  using rt.core.business.security.interfaces;
  using rt.core.model.core;
  using rt.srz.business.manager;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  using IPermissionManager = rt.core.business.manager.IPermissionManager;
  using IPermissionRoleManager = rt.core.business.manager.IPermissionRoleManager;
  using IRoleManager = rt.core.business.manager.IRoleManager;
  using IUserGroupManager = rt.core.business.manager.IUserGroupManager;
  using IUserGroupRoleManager = rt.core.business.manager.IUserGroupRoleManager;
  using IUserManager = rt.core.business.manager.IUserManager;

  #endregion

  /// <summary>
  ///   The security service.
  /// </summary>
  public class SecurityService : ISecurityService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление группы
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid AddGroup(string name)
    {
      return ObjectFactory.GetInstance<IGroupManager>().AddGroup(name);
    }

    /// <summary>
    /// Добавление разрешения
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
      return ObjectFactory.GetInstance<IPermissionManager>().AddPermission(code, name);
    }

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
      return ObjectFactory.GetInstance<IRoleManager>().AddRole(name);
    }

    /// <summary>
    /// Добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    public User AddUser(User user)
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().AddUser(user);
    }

    /// <summary>
    /// Добавление пользователя в группы, удаление из групп
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignGroups">
    /// Группы в которые добавляется пользователь 
    /// </param>
    /// <param name="detachGroups">
    /// Группы из которых исключается пользователь 
    /// </param>
    public void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups)
    {
      ObjectFactory.GetInstance<IUserGroupManager>().AssignGroupsToUser(userId, assignGroups, detachGroups);
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
      ObjectFactory.GetInstance<IUserManager>().AssignPdpToUser(userId, pdpId);
    }

    /// <summary>
    /// Изменение (назначение, отсоединение) разрешений для роли
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <param name="assignPermissions">
    /// назначаемые разрешения 
    /// </param>
    /// <param name="detachPermissions">
    /// отсоединяемые разрешения 
    /// </param>
    public void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>().AssignPermissionsToRole(roleId, assignPermissions, detachPermissions);
    }

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    public void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().AssignRolesToGroup(groupId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Назначение, отсоединение ролей для разрешения
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <param name="assignRoles">
    /// назначаемые роли 
    /// </param>
    /// <param name="detachRoles">
    /// отсоединяемые роли 
    /// </param>
    public void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>().AssignRolesToPermission(permissionId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    public void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().AssignRolesToUser(userId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Добавление пользователей в группу, удаление пользователей из группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignUsers">
    /// </param>
    /// <param name="detachUsers">
    /// </param>
    public void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers)
    {
      ObjectFactory.GetInstance<IUserGroupManager>().AssignUsersToGroup(groupId, assignUsers, detachUsers);
    }

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    public void DeleteGroup(Guid groupId)
    {
      ObjectFactory.GetInstance<IGroupManager>().DeleteGroup(groupId);
    }

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeletePermission(Guid id)
    {
      ObjectFactory.GetInstance<IPermissionManager>().DeletePermission(id);
    }

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteRole(Guid id)
    {
      ObjectFactory.GetInstance<IRoleManager>().DeleteRole(id);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    public void DeleteUser(Guid userId)
    {
      ObjectFactory.GetInstance<IUserManager>().DeleteUser(userId);
    }

    /// <summary>
    /// Проверяет есть ли в базе разрешение с указанным ид и кодом
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
      return ObjectFactory.GetInstance<IPermissionManager>().ExistsPermissionCode(permissionId, code);
    }

    /// <summary>
    ///   Возвращает текущего пользователя
    /// </summary>
    /// <returns> The <see cref="User" /> . </returns>
    public User GetCurrentUser()
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().GetCurrentUser();
    }

    /// <summary>
    /// Получает группу по идентификатору
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="Group"/> . 
    /// </returns>
    public Group GetGroup(Guid groupId)
    {
      return ObjectFactory.GetInstance<IGroupManager>().GetById(groupId);
    }

    /// <summary>
    ///   Список всех групп
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Group> GetGroups()
    {
      return ObjectFactory.GetInstance<IGroupManager>().GetAll();
    }

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Group> GetGroupsByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IGroupManager>().GetGroupsByNameContains(contains);
    }

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Group> GetGroupsByUser(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserGroupManager>().GetGroupsByUser(userId);
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
      return ObjectFactory.GetInstance<IUserManager>().GetIsUserAllowPermission(userId, permissionCode);
    }

    /// <summary>
    /// Разрешено ли текущему пользователю разрешение
    /// </summary>
    /// <param name="permissionCode"></param>
    /// <returns></returns>
    public bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode)
    {
      User currentUser = GetCurrentUser();
      return ObjectFactory.GetInstance<IUserManager>().GetIsUserAllowPermission(currentUser.Id, (int)permissionCode);
    }

    /// <summary>
    /// Является ли текущий пользователь админом территориального фонда
    /// </summary>
    /// <returns></returns>
    public bool IsCurrentUserAdminTF()
    {
      return IsUserAdminTF(GetCurrentUser().Id);
    }

    /// <summary>
    /// Является ли текущий пользователь админом СМО
    /// </summary>
    /// <returns></returns>
    public bool IsCurrentUserAdminSmo()
    {
      return IsUserAdminSmo(GetCurrentUser().Id);
    }

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminTF(Guid userId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().IsUserAdminTF(userId);
    }

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminSmo(Guid userId)
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().IsUserAdminSmo(userId);
    }

    /// <summary>
    /// Получает разрешение по идентификатору
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="Permission"/> . 
    /// </returns>
    public Permission GetPermission(Guid permissionId)
    {
      return ObjectFactory.GetInstance<IPermissionManager>().GetById(permissionId);
    }

    /// <summary>
    ///   Получает список всех разрешений
    /// </summary>
    /// <param name="roleId"> </param>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Permission> GetPermissions()
    {
      return ObjectFactory.GetInstance<IPermissionManager>().GetAll();
    }

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Permission> GetPermissionsByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IPermissionManager>().GetPermissionsByNameContains(contains);
    }

    /// <summary>
    /// Получает роль по идентификатору
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="Role"/> . 
    /// </returns>
    public Role GetRole(Guid roleId)
    {
      return ObjectFactory.GetInstance<IRoleManager>().GetById(roleId);
    }

    /// <summary>
    /// Получает список разрешений для роли
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Permission> GetRolePermissions(Guid roleId)
    {
      return ObjectFactory.GetInstance<IPermissionRoleManager>().GetRolePermissions(roleId);
    }

    /// <summary>
    ///   Получает список ролей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Role> GetRoles()
    {
      return ObjectFactory.GetInstance<IRoleManager>().GetAll();
    }

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByGroup(Guid groupId)
    {
      return ObjectFactory.GetInstance<IUserGroupRoleManager>().GetRolesByGroup(groupId);
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
      return ObjectFactory.GetInstance<IRoleManager>().GetRolesByNameContains(contains);
    }

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByPermission(Guid permissionId)
    {
      return ObjectFactory.GetInstance<IPermissionRoleManager>().GetRolesByPermission(permissionId);
    }

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByUser(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserGroupRoleManager>().GetRolesByUser(userId);
    }

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> . 
    /// </returns>
    public Setting GetSetting(string name)
    {
      return ObjectFactory.GetInstance<ISettingManager>().GetSetting(name);
    }

    /// <summary>
    /// Возвращает пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id. 
    /// </param>
    /// <returns>
    /// The <see cref="User"/> . 
    /// </returns>
    public User GetUser(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserManager>().GetById(userId);
    }

    /// <summary>
    /// Возвращает пользователя по имени
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    public User GetUserByName(string name)
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().GetUserByName(name);
    }

    /// <summary>
    /// Возвращает имя пользователя по email
    /// </summary>
    /// <param name="email">
    /// </param>
    /// <returns>
    /// UserName 
    /// </returns>
    public string GetUserNameByEmail(string email)
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().GetUserNameByEmail(email);
    }

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsers()
    {
      return ObjectFactory.GetInstance<IUserManager>().GetUsers();
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      return ObjectFactory.GetInstance<IOrganisationManager>().GetUsersByCurrent();
    }

   

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<User> GetUsersByGroup(Guid groupId)
    {
      return ObjectFactory.GetInstance<IUserGroupManager>().GetUsersByGroup(groupId);
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
      return ObjectFactory.GetInstance<IUserManager>().GetUsersByNameContains(contains);
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
      return ObjectFactory.GetInstance<IUserManager>().IsUserHasAdminPermissions(user);
    }

    /// <summary>
    /// Сохранение или добавление группы
    /// </summary>
    /// <param name="group">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid SaveGroup(Group @group)
    {
      ObjectFactory.GetInstance<IGroupManager>().SaveOrUpdate(group);
      return group.Id;
    }

    /// <summary>
    /// Добавляет или сохраняет разрешение
    /// </summary>
    /// <param name="permission">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid SavePermission(Permission permission)
    {
      ObjectFactory.GetInstance<IPermissionManager>().SaveOrUpdate(permission);
      return permission.Id;
    }

    /// <summary>
    /// Добавляет или сохраняет роль
    /// </summary>
    /// <param name="role">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    public Guid SaveRole(Role role)
    {
      ObjectFactory.GetInstance<IRoleManager>().SaveOrUpdate(role);
      return role.Id;
    }

    /// <summary>
    /// Сохраняет или добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// The <see cref="User"/> . 
    /// </returns>
    public User SaveUser(User user)
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().SaveUser(user);
    }

    /// <summary>
    /// Обновляет пароль пользователя
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <param name="newPassword">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    public User UpdatePassword(string name, string newPassword)
    {
      return ObjectFactory.GetInstance<ISecurityProvider>().UpdatePassword(name, newPassword);
    }

    #endregion
  }
}