// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityGateInternal.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The security gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Security
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.services.aspects;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;
  using rt.srz.model.enumerations;

  #endregion

  /// <summary>
  ///   The security gate.
  /// </summary>
  public class SecurityGateInternal : InterceptedBase, ISecurityService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    private readonly ISecurityService Service = new SecurityService();

    #endregion

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
      return InvokeInterceptors(() => Service.AddGroup(name));
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
      return InvokeInterceptors(() => Service.AddPermission(code, name));
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
      return InvokeInterceptors(() => Service.AddRole(name));
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
      return InvokeInterceptors(() => Service.AddUser(user));
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
      InvokeInterceptors(() => Service.AssignGroupsToUser(userId, assignGroups, detachGroups));
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
      InvokeInterceptors(() => Service.AssignPdpToUser(userId, pdpId));
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
      InvokeInterceptors(() => Service.AssignPermissionsToRole(roleId, assignPermissions, detachPermissions));
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
      InvokeInterceptors(() => Service.AssignRolesToGroup(groupId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignRolesToPermission(permissionId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignRolesToUser(userId, assignRoles, detachRoles));
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
      InvokeInterceptors(() => Service.AssignUsersToGroup(groupId, assignUsers, detachUsers));
    }

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    public void DeleteGroup(Guid groupId)
    {
      InvokeInterceptors(() => Service.DeleteGroup(groupId));
    }

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeletePermission(Guid id)
    {
      InvokeInterceptors(() => Service.DeletePermission(id));
    }

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeleteRole(Guid id)
    {
      InvokeInterceptors(() => Service.DeleteRole(id));
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    public void DeleteUser(Guid userId)
    {
      InvokeInterceptors(() => Service.DeleteUser(userId));
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
      return InvokeInterceptors(() => Service.ExistsPermissionCode(permissionId, code));
    }

    /// <summary>
    ///   The get current user.
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
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Group> GetGroups()
    {
      return InvokeInterceptors(() => Service.GetGroups());
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
      return InvokeInterceptors(() => Service.GetGroupsByNameContains(contains));
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
      return InvokeInterceptors(() => Service.GetGroupsByUser(userId));
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
      return InvokeInterceptors(() => Service.GetIsUserAllowPermission(userId, permissionCode));
    }

    /// <summary>
    /// Разрешено ли текущему пользователю разрешение
    /// </summary>
    /// <param name="permissionCode"></param>
    /// <returns></returns>
    public bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode)
    {
      return InvokeInterceptors(() => Service.GetIsCurrentUserAllowPermission(permissionCode));
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
      return InvokeInterceptors(() => Service.GetPermission(permissionId));
    }

    /// <summary>
    ///   Получает список всех разрешений
    /// </summary>
    /// <param name="roleId"> </param>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Permission> GetPermissions()
    {
      return InvokeInterceptors(() => Service.GetPermissions());
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
      return InvokeInterceptors(() => Service.GetPermissionsByNameContains(contains));
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
      return InvokeInterceptors(() => Service.GetRole(roleId));
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
      return InvokeInterceptors(() => Service.GetRolePermissions(roleId));
    }

    /// <summary>
    ///   Получает список ролей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<Role> GetRoles()
    {
      return InvokeInterceptors(() => Service.GetRoles());
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
      return InvokeInterceptors(() => Service.GetRolesByGroup(groupId));
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
      return InvokeInterceptors(() => Service.GetRolesByNameContains(contains));
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
      return InvokeInterceptors(() => Service.GetRolesByPermission(permissionId));
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
      return InvokeInterceptors(() => Service.GetRolesByUser(userId));
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
      return InvokeInterceptors(() => Service.GetSetting(name));
    }

    /// <summary>
    /// Возвращает пользователя
    /// </summary>
    /// <param name="userId">
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
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsers()
    {
      return InvokeInterceptors(() => Service.GetUsers());
    }

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    public IList<User> GetUsersByCurrent()
    {
      return InvokeInterceptors(() => Service.GetUsersByCurrent());
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
      return InvokeInterceptors(() => Service.GetUsersByGroup(groupId));
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
      return InvokeInterceptors(() => Service.GetUsersByNameContains(contains));
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
      return InvokeInterceptors(() => Service.IsUserHasAdminPermissions(user));
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
      return InvokeInterceptors(() => Service.SaveGroup(@group));
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
      return InvokeInterceptors(() => Service.SavePermission(permission));
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
      return InvokeInterceptors(() => Service.SaveRole(role));
    }

    /// <summary>
    /// Добавляет или сохраняет пользователя
    /// </summary>
    /// <param name="user">
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
    /// </param>
    /// <param name="newPassword">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    public User UpdatePassword(string name, string newPassword)
    {
      return InvokeInterceptors(() => Service.UpdatePassword(name, newPassword));
    }

    /// <summary>
    /// Является ли текущий пользователь админом территориального фонда
    /// </summary>
    /// <returns></returns>
    public bool IsCurrentUserAdminTF()
    {
      return InvokeInterceptors(() => Service.IsCurrentUserAdminTF());
    }

    /// <summary>
    /// Является ли текущий пользователь админом СМО
    /// </summary>
    /// <returns></returns>
    public bool IsCurrentUserAdminSmo()
    {
      return InvokeInterceptors(() => Service.IsCurrentUserAdminSmo());
    }

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminTF(Guid userId)
    {
      return InvokeInterceptors(() => Service.IsUserAdminTF(userId));
    }

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    public bool IsUserAdminSmo(Guid userId)
    {
      return InvokeInterceptors(() => Service.IsUserAdminSmo(userId));
    }


    #endregion
  }
}