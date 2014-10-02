// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The SecurityService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.srz.model.srz;
  using rt.srz.model.enumerations;

  #endregion

  /// <summary>
  ///   The SecurityService interface.
  /// </summary>
  [ServiceContract]
  public interface ISecurityService
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
    [OperationContract]
    Guid AddGroup(string name);

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
    [OperationContract]
    Guid AddPermission(int code, string name);

    /// <summary>
    /// Добавление роли
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid AddRole(string name);

    /// <summary>
    /// Добавляет пользователя
    /// </summary>
    /// <param name="user">
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
    /// </param>
    /// <param name="pdpId">
    /// </param>
    [OperationContract]
    void AssignPdpToUser(Guid userId, Guid? pdpId);

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
    [OperationContract]
    void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions);

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    [OperationContract]
    void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles);

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
    [OperationContract]
    void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    [OperationContract]
    void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// Добавление пользователей в группу, удаление пользователей из группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignUsers">
    /// </param>
    /// <param name="detachUsers">
    /// </param>
    [OperationContract]
    void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers);

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    [OperationContract]
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// </param>
    [OperationContract]
    void DeletePermission(Guid id);

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// </param>
    [OperationContract]
    void DeleteRole(Guid id);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    [OperationContract]
    void DeleteUser(Guid userId);

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
    /// </param>
    /// <returns>
    /// The <see cref="Group"/> . 
    /// </returns>
    [OperationContract]
    Group GetGroup(Guid groupId);

    /// <summary>
    ///   Список всех групп
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Group> GetGroups();

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Group> GetGroupsByNameContains(string contains);

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Group> GetGroupsByUser(Guid userId);

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
    [OperationContract]
    bool GetIsUserAllowPermission(Guid userId, int permissionCode);

    /// <summary>
    /// Разрешено ли текущему пользователю разрешение
    /// </summary>
    /// <param name="permissionCode"></param>
    /// <returns></returns>
    [OperationContract]
    bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode);

    /// <summary>
    /// Получает разрешение по идентификатору
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="Permission"/> . 
    /// </returns>
    [OperationContract]
    Permission GetPermission(Guid permissionId);

    /// <summary>
    ///   Получает список всех разрешений
    /// </summary>
    /// <param name="roleId"> </param>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Permission> GetPermissions();

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Permission> GetPermissionsByNameContains(string contains);

    /// <summary>
    /// Получает роль по идентификатору
    /// </summary>
    /// <param name="roleId">
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
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Permission> GetRolePermissions(Guid roleId);

    /// <summary>
    ///   Получает список всех ролей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Role> GetRoles();

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByGroup(Guid groupId);

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByNameContains(string contains);

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByPermission(Guid permissionId);

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByUser(Guid userId);

    /// <summary>
    /// The get setting.
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Setting"/> . 
    /// </returns>
    [OperationContract]
    Setting GetSetting(string name);

    /// <summary>
    /// Возвращает пользователя
    /// </summary>
    /// <param name="userId">
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
    /// </param>
    /// <returns>
    /// UserName 
    /// </returns>
    [OperationContract]
    string GetUserNameByEmail(string email);

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<User> GetUsers();

    /// <summary>
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<User> GetUsersByCurrent();

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<User> GetUsersByGroup(Guid groupId);

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<User> GetUsersByNameContains(string contains);

    /// <summary>
    /// Имеет ли пользователь роль администратора или входит в группы любая из которых имеет роль администратора
    /// </summary>
    /// <param name="user">
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
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid SaveGroup(Group @group);

    /// <summary>
    /// Добавляет или сохраняет разрешение
    /// </summary>
    /// <param name="permission">
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
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid SaveRole(Role role);

    /// <summary>
    /// The save user.
    /// </summary>
    /// <param name="user">
    /// The user. 
    /// </param>
    /// <returns>
    /// The <see cref="User"/> . 
    /// </returns>
    User SaveUser(User user);

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
    [OperationContract]
    User UpdatePassword(string name, string newPassword);

    /// <summary>
    /// Является ли текущий пользователь админом территориального фонда
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    bool IsCurrentUserAdminTF();
    
    /// <summary>
    /// Является ли текущий пользователь админом СМО
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    bool IsCurrentUserAdminSmo();

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    [OperationContract]
    bool IsUserAdminTF(Guid userId);

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    [OperationContract]
    bool IsUserAdminSmo(Guid userId);

    #endregion
  }
}