// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SecurityService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.core.model.core;

  #endregion

  /// <summary>
  ///   The SecurityService interface.
  /// </summary>
  [ServiceContract]
  public interface ISecurityService
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
    [OperationContract]
    User AddUser(User user);

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
    [OperationContract]
    void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups);

    /// <summary>
    /// Назначение пункта выдачи пользователю
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    [OperationContract]
    void AssignPdpToUser(Guid userId, Guid? pdpId);

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
    [OperationContract]
    void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions);

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
    [OperationContract]
    void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles);

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
    [OperationContract]
    void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles);

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
    [OperationContract]
    void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles);

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
    [OperationContract]
    void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers);

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    [OperationContract]
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeletePermission(Guid id);

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeleteRole(Guid id);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    [OperationContract]
    void DeleteUser(Guid userId);

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
    [OperationContract]
    bool ExistsPermissionCode(Guid permissionId, int code);

    /// <summary>
    ///   Возвращает текущего пользователя
    /// </summary>
    /// <returns> The <see cref="User" /> . </returns>
    [OperationContract]
    User GetCurrentUser();

    /// <summary>
    /// Получает группу по идентификатору
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="Group"/> .
    /// </returns>
    [OperationContract]
    Group GetGroup(Guid groupId);

    /// <summary>
    ///   Список всех групп
    /// </summary>
    /// <returns> The <see cref="List{Group}" /> . </returns>
    [OperationContract]
    List<Group> GetGroups();

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    [OperationContract]
    List<Group> GetGroupsByNameContains(string contains);

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    ///   The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Group}"/> .
    /// </returns>
    [OperationContract]
    List<Group> GetGroupsByUser(Guid userId);

    /// <summary>
    /// Разрешено ли текущему пользователю разрешение
    /// </summary>
    /// <param name="permissionCode">
    /// The permission Code.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [OperationContract]
    bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode);

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
    [OperationContract]
    bool GetIsUserAllowPermission(Guid userId, int permissionCode);

    /// <summary>
    /// Получает разрешение по идентификатору
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
    /// </param>
    /// <returns>
    /// The <see cref="Permission"/> .
    /// </returns>
    [OperationContract]
    Permission GetPermission(Guid permissionId);

    /// <summary>
    ///   Получает список всех разрешений
    /// </summary>
    /// <returns> The <see cref="List{Permission}" /> . </returns>
    [OperationContract]
    List<Permission> GetPermissions();

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    [OperationContract]
    List<Permission> GetPermissionsByNameContains(string contains);

    /// <summary>
    /// Получает роль по идентификатору
    /// </summary>
    /// <param name="roleId">
    /// The role Id.
    /// </param>
    /// <returns>
    /// The <see cref="Role"/> .
    /// </returns>
    [OperationContract]
    Role GetRole(Guid roleId);

    /// <summary>
    /// Получает список разрешений для роли
    /// </summary>
    /// <param name="roleId">
    ///   The role Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Permission}"/> .
    /// </returns>
    [OperationContract]
    List<Permission> GetRolePermissions(Guid roleId);

    /// <summary>
    ///   Получает список ролей
    /// </summary>
    /// <returns> The <see cref="List{Role}" /> . </returns>
    [OperationContract]
    List<Role> GetRoles();

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    ///   The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    [OperationContract]
    List<Role> GetRolesByGroup(Guid groupId);

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    [OperationContract]
    List<Role> GetRolesByNameContains(string contains);

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
    /// </summary>
    /// <param name="permissionId">
    ///   The permission Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    [OperationContract]
    List<Role> GetRolesByPermission(Guid permissionId);

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    ///   The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{Role}"/> .
    /// </returns>
    [OperationContract]
    List<Role> GetRolesByUser(Guid userId);

    /// <summary>
    /// Возвращает пользователя
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="User"/> .
    /// </returns>
    [OperationContract]
    User GetUser(Guid userId);

    /// <summary>
    /// Возвращает пользователя по имени
    /// </summary>
    /// <param name="name">
    /// The name.
    /// </param>
    /// <returns>
    /// User
    /// </returns>
    [OperationContract]
    User GetUserByName(string name);

    /// <summary>
    /// Возвращает имя пользователя по email
    /// </summary>
    /// <param name="email">
    /// The email.
    /// </param>
    /// <returns>
    /// UserName
    /// </returns>
    [OperationContract]
    string GetUserNameByEmail(string email);

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    [OperationContract]
    List<User> GetUsers();

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    ///   The group Id.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    [OperationContract]
    List<User> GetUsersByGroup(Guid groupId);

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    ///   The contains.
    /// </param>
    /// <returns>
    /// The <see cref="List{User}"/> .
    /// </returns>
    [OperationContract]
    List<User> GetUsersByNameContains(string contains);

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [OperationContract]
    bool IsUserAdminSmo(Guid userId);

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    [OperationContract]
    bool IsUserAdminTfoms(Guid userId);

    /// <summary>
    /// Имеет ли пользователь роль администратора или входит в группы любая из которых имеет роль администратора
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    [OperationContract]
    bool IsUserHasAdminPermissions(User user);

    /// <summary>
    /// Сохранение или добавление группы
    /// </summary>
    /// <param name="group">
    /// The group.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveGroup(Group group);

    /// <summary>
    /// Добавляет или сохраняет разрешение
    /// </summary>
    /// <param name="permission">
    /// The permission.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SavePermission(Permission permission);

    /// <summary>
    /// Добавляет или сохраняет роль
    /// </summary>
    /// <param name="role">
    /// The role.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    [OperationContract]
    Guid SaveRole(Role role);

    /// <summary>
    /// Сохраняет или добавляет пользователя
    /// </summary>
    /// <param name="user">
    /// The user.
    /// </param>
    /// <returns>
    /// The <see cref="User"/> .
    /// </returns>
    [OperationContract]
    User SaveUser(User user);

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
    [OperationContract]
    User UpdatePassword(string name, string newPassword);

    #endregion
  }
}