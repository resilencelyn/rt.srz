// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.core.business.manager;
  using rt.core.business.security.interfaces;
  using rt.core.model;
  using rt.core.model.core;
  using rt.core.model.interfaces;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The security service.
  /// </summary>
  public class SecurityService : ISecurityService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// The user.
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
    /// The user Id.
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
    /// The user Id.
    /// </param>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    public void AssignPdpToUser(Guid userId, Guid? pdpId)
    {
      ObjectFactory.GetInstance<IUserManager>().AssignPdpToUser(userId, pdpId);
    }

    /// <summary>
    /// Изменение (назначение, отсоединение) разрешений для роли
    /// </summary>
    /// <param name="roleId">
    /// The role Id.
    /// </param>
    /// <param name="assignPermissions">
    /// назначаемые разрешения
    /// </param>
    /// <param name="detachPermissions">
    /// отсоединяемые разрешения
    /// </param>
    public void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>()
                   .AssignPermissionsToRole(roleId, assignPermissions, detachPermissions);
    }

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    /// <param name="assignRoles">
    /// The assign Roles.
    /// </param>
    /// <param name="detachRoles">
    /// The detach Roles.
    /// </param>
    public void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().AssignRolesToGroup(groupId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Назначение, отсоединение ролей для разрешения
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
    /// </param>
    /// <param name="assignRoles">
    /// назначаемые роли
    /// </param>
    /// <param name="detachRoles">
    /// отсоединяемые роли
    /// </param>
    public void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>()
                   .AssignRolesToPermission(permissionId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="assignRoles">
    /// The assign Roles.
    /// </param>
    /// <param name="detachRoles">
    /// The detach Roles.
    /// </param>
    public void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().AssignRolesToUser(userId, assignRoles, detachRoles);
    }

    /// <summary>
    /// Добавление пользователей в группу, удаление пользователей из группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    /// <param name="assignUsers">
    /// The assign Users.
    /// </param>
    /// <param name="detachUsers">
    /// The detach Users.
    /// </param>
    public void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers)
    {
      ObjectFactory.GetInstance<IUserGroupManager>().AssignUsersToGroup(groupId, assignUsers, detachUsers);
    }

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    public void DeleteGroup(Guid groupId)
    {
      ObjectFactory.GetInstance<IGroupManager>().DeleteGroup(groupId);
    }

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeletePermission(Guid id)
    {
      ObjectFactory.GetInstance<IPermissionManager>().DeletePermission(id);
    }

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteRole(Guid id)
    {
      ObjectFactory.GetInstance<IRoleManager>().DeleteRole(id);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    public void DeleteUser(Guid userId)
    {
      ObjectFactory.GetInstance<IUserManager>().DeleteUser(userId);
    }

    /// <summary>
    /// Проверяет есть ли в базе разрешение с указанным ид и кодом
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
    /// </param>
    /// <param name="code">
    /// The code.
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
    /// The group Id.
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
    /// <returns> The <see cref="List{Group}" /> . </returns>
    public List<Group> GetGroups()
    {
      return ObjectFactory.GetInstance<IGroupManager>().GetAll().ToList();
    }

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    public List<Group> GetGroupsByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IGroupManager>().GetGroupsByNameContains(contains).ToList();
    }

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    ///   The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    public List<Group> GetGroupsByUser(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserGroupManager>().GetGroupsByUser(userId).ToList();
    }

    /// <summary>
    /// Разрешено ли текущему пользователю разрешение
    /// </summary>
    /// <param name="permissionCode">
    /// The permission Code.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode)
    {
      var currentUser = GetCurrentUser();
      return ObjectFactory.GetInstance<IUserManager>().GetIsUserAllowPermission(currentUser.Id, (int)permissionCode);
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
    /// The <see cref="bool"/> .
    /// </returns>
    public bool GetIsUserAllowPermission(Guid userId, int permissionCode)
    {
      return ObjectFactory.GetInstance<IUserManager>().GetIsUserAllowPermission(userId, permissionCode);
    }

    /// <summary>
    /// Получает разрешение по идентификатору
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
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
    /// <returns> The <see cref="List{Permission}" /> . </returns>
    public List<Permission> GetPermissions()
    {
      return ObjectFactory.GetInstance<IPermissionManager>().GetAll().ToList();
    }

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    public List<Permission> GetPermissionsByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IPermissionManager>().GetPermissionsByNameContains(contains).ToList();
    }

    /// <summary>
    /// Получает роль по идентификатору
    /// </summary>
    /// <param name="roleId">
    /// The role Id.
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
    ///   The role Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    public List<Permission> GetRolePermissions(Guid roleId)
    {
      return ObjectFactory.GetInstance<IPermissionRoleManager>().GetRolePermissions(roleId).ToList();
    }

    /// <summary>
    ///   Получает список ролей
    /// </summary>
    /// <returns> The <see cref="List{Role}" /> . </returns>
    public List<Role> GetRoles()
    {
      return ObjectFactory.GetInstance<IRoleManager>().GetAll().ToList();
    }

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    ///   The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByGroup(Guid groupId)
    {
      return ObjectFactory.GetInstance<IUserGroupRoleManager>().GetRolesByGroup(groupId).ToList();
    }

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IRoleManager>().GetRolesByNameContains(contains).ToList();
    }

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
    /// </summary>
    /// <param name="permissionId">
    ///   The permission Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByPermission(Guid permissionId)
    {
      return ObjectFactory.GetInstance<IPermissionRoleManager>().GetRolesByPermission(permissionId).ToList();
    }

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    ///   The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByUser(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserGroupRoleManager>().GetRolesByUser(userId).ToList();
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
    /// The name.
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
    /// The email.
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
    /// <returns> The <see cref="List{User}" /> . </returns>
    public List<User> GetUsers()
    {
      return ObjectFactory.GetInstance<IUserManager>().GetUsers().ToList();
    }

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    ///   The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    public List<User> GetUsersByGroup(Guid groupId)
    {
      return ObjectFactory.GetInstance<IUserGroupManager>().GetUsersByGroup(groupId).ToList();
    }

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    public List<User> GetUsersByNameContains(string contains)
    {
      return ObjectFactory.GetInstance<IUserManager>().GetUsersByNameContains(contains).ToList();
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
      return ObjectFactory.GetInstance<IUserManager>().IsUserAdminSmo(userId);
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
    public bool IsUserAdminTfoms(Guid userId)
    {
      return ObjectFactory.GetInstance<IUserManager>().IsUserAdminTf(userId);
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
      return ObjectFactory.GetInstance<IUserManager>().IsUserHasAdminPermissions(user);
    }

    /// <summary>
    /// Сохранение или добавление группы
    /// </summary>
    /// <param name="group">
    /// The group.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    public Guid SaveGroup(Group group)
    {
      ObjectFactory.GetInstance<IGroupManager>().SaveOrUpdate(group);
      return group.Id;
    }

    /// <summary>
    /// Добавляет или сохраняет разрешение
    /// </summary>
    /// <param name="permission">
    /// The permission.
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
    /// The role.
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
    /// The user.
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
    /// The name.
    /// </param>
    /// <param name="newPassword">
    /// The new Password.
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