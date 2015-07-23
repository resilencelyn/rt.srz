// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityClient.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The security gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model;
  using rt.core.model.core;
  using rt.core.model.interfaces;
  using rt.core.services.registry;

  #endregion

  /// <summary>
  ///   The security gate.
  /// </summary>
  public class SecurityClient : ServiceClient<ISecurityService>, ISecurityService
  {
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
      return InvokeInterceptors(() => Service.AddUser(user));
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
      InvokeInterceptors(() => Service.AssignGroupsToUser(userId, assignGroups, detachGroups));
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
      InvokeInterceptors(() => Service.AssignPdpToUser(userId, pdpId));
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
      InvokeInterceptors(() => Service.AssignPermissionsToRole(roleId, assignPermissions, detachPermissions));
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
      InvokeInterceptors(() => Service.AssignRolesToGroup(groupId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignRolesToPermission(permissionId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignRolesToUser(userId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignUsersToGroup(groupId, assignUsers, detachUsers));
    }

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    public void DeleteGroup(Guid groupId)
    {
      InvokeInterceptors(() => Service.DeleteGroup(groupId));
    }

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeletePermission(Guid id)
    {
      InvokeInterceptors(() => Service.DeletePermission(id));
    }

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    public void DeleteRole(Guid id)
    {
      InvokeInterceptors(() => Service.DeleteRole(id));
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    public void DeleteUser(Guid userId)
    {
      InvokeInterceptors(() => Service.DeleteUser(userId));
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
      return InvokeInterceptors(() => Service.ExistsPermissionCode(permissionId, code));
    }

    /// <summary>
    ///   Возвращает текущего пользователя
    /// </summary>
    /// <returns> The <see cref="User" /> . </returns>
    public User GetCurrentUser()
    {
      return InvokeInterceptors(() => Service.GetCurrentUser());
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
      return InvokeInterceptors(() => Service.GetGroup(groupId));
    }

    /// <summary>
    ///   Список всех групп
    /// </summary>
    /// <returns> The <see cref="List{Group}" /> . </returns>
    public List<Group> GetGroups()
    {
      return InvokeInterceptors(() => Service.GetGroups());
    }

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    public List<Group> GetGroupsByNameContains(string contains)
    {
      return InvokeInterceptors(() => Service.GetGroupsByNameContains(contains));
    }

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    public List<Group> GetGroupsByUser(Guid userId)
    {
      return InvokeInterceptors(() => Service.GetGroupsByUser(userId));
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
      return InvokeInterceptors(() => Service.GetIsCurrentUserAllowPermission(permissionCode));
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
      return InvokeInterceptors(() => Service.GetIsUserAllowPermission(userId, permissionCode));
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
      return InvokeInterceptors(() => Service.GetPermission(permissionId));
    }

    /// <summary>
    ///   Получает список всех разрешений
    /// </summary>
    /// <returns> The <see cref="List{Permission}" /> . </returns>
    public List<Permission> GetPermissions()
    {
      return InvokeInterceptors(() => Service.GetPermissions());
    }

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    public List<Permission> GetPermissionsByNameContains(string contains)
    {
      return InvokeInterceptors(() => Service.GetPermissionsByNameContains(contains));
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
      return InvokeInterceptors(() => Service.GetRole(roleId));
    }

    /// <summary>
    /// Получает список разрешений для роли
    /// </summary>
    /// <param name="roleId">
    /// The role Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    public List<Permission> GetRolePermissions(Guid roleId)
    {
      return InvokeInterceptors(() => Service.GetRolePermissions(roleId));
    }

    /// <summary>
    ///   Получает список ролей
    /// </summary>
    /// <returns> The <see cref="List{Role}" /> . </returns>
    public List<Role> GetRoles()
    {
      return InvokeInterceptors(() => Service.GetRoles());
    }

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByGroup(Guid groupId)
    {
      return InvokeInterceptors(() => Service.GetRolesByGroup(groupId));
    }

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByNameContains(string contains)
    {
      return InvokeInterceptors(() => Service.GetRolesByNameContains(contains));
    }

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByPermission(Guid permissionId)
    {
      return InvokeInterceptors(() => Service.GetRolesByPermission(permissionId));
    }

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    public List<Role> GetRolesByUser(Guid userId)
    {
      return InvokeInterceptors(() => Service.GetRolesByUser(userId));
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
      return InvokeInterceptors(() => Service.GetUser(userId));
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
      return InvokeInterceptors(() => Service.GetUserByName(name));
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
      return InvokeInterceptors(() => Service.GetUserNameByEmail(email));
    }

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    public List<User> GetUsers()
    {
      return InvokeInterceptors(() => Service.GetUsers());
    }

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    public List<User> GetUsersByGroup(Guid groupId)
    {
      return InvokeInterceptors(() => Service.GetUsersByGroup(groupId));
    }

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    public List<User> GetUsersByNameContains(string contains)
    {
      return InvokeInterceptors(() => Service.GetUsersByNameContains(contains));
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
      return InvokeInterceptors(() => Service.IsUserAdminSmo(userId));
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
      return InvokeInterceptors(() => Service.IsUserAdminTfoms(userId));
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
      return InvokeInterceptors(() => Service.IsUserHasAdminPermissions(user));
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
    public Guid SaveGroup(Group @group)
    {
      return InvokeInterceptors(() => Service.SaveGroup(@group));
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
      return InvokeInterceptors(() => Service.SavePermission(permission));
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
      return InvokeInterceptors(() => Service.SaveRole(role));
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
      return InvokeInterceptors(() => Service.SaveUser(user));
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
      return InvokeInterceptors(() => Service.UpdatePassword(name, newPassword));
    }
  }
}