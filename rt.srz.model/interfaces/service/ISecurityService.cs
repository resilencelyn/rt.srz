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
    /// ���������� ������
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid AddGroup(string name);

    /// <summary>
    /// ���������� ����������
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
    /// ���������� ����
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
    /// ��������� ������������
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    [OperationContract]
    User AddUser(User user);

    /// <summary>
    /// ���������� ������������ � ������, �������� �� �����
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignGroups">
    /// ������ � ������� ����������� ������������ 
    /// </param>
    /// <param name="detachGroups">
    /// ������ �� ������� ����������� ������������ 
    /// </param>
    [OperationContract]
    void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups);

    /// <summary>
    /// ���������� ������ ������ ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="pdpId">
    /// </param>
    [OperationContract]
    void AssignPdpToUser(Guid userId, Guid? pdpId);

    /// <summary>
    /// ��������� (����������, ������������) ���������� ��� ����
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <param name="assignPermissions">
    /// ����������� ���������� 
    /// </param>
    /// <param name="detachPermissions">
    /// ������������� ���������� 
    /// </param>
    [OperationContract]
    void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions);

    /// <summary>
    /// ���������� ������������ �����
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
    /// ����������, ������������ ����� ��� ����������
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <param name="assignRoles">
    /// ����������� ���� 
    /// </param>
    /// <param name="detachRoles">
    /// ������������� ���� 
    /// </param>
    [OperationContract]
    void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// ���������� ������������ �����
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
    /// ���������� ������������� � ������, �������� ������������� �� ������
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
    /// �������� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    [OperationContract]
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// �������� ����������
    /// </summary>
    /// <param name="id">
    /// </param>
    [OperationContract]
    void DeletePermission(Guid id);

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="id">
    /// </param>
    [OperationContract]
    void DeleteRole(Guid id);

    /// <summary>
    /// �������� ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    [OperationContract]
    void DeleteUser(Guid userId);

    /// <summary>
    /// ��������� ���� �� � ���� ���������� � ��������� �� � �����
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
    ///   ���������� �������� ������������
    /// </summary>
    /// <returns> The <see cref="User" /> . </returns>
    [OperationContract]
    User GetCurrentUser();

    /// <summary>
    /// �������� ������ �� ��������������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="Group"/> . 
    /// </returns>
    [OperationContract]
    Group GetGroup(Guid groupId);

    /// <summary>
    ///   ������ ���� �����
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Group> GetGroups();

    /// <summary>
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Group> GetGroupsByNameContains(string contains);

    /// <summary>
    /// �������� ������ ���� �����, ���� ������ ������ ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Group> GetGroupsByUser(Guid userId);

    /// <summary>
    /// ��������� �� ������������ ���������� � ��������� �����
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
    /// ��������� �� �������� ������������ ����������
    /// </summary>
    /// <param name="permissionCode"></param>
    /// <returns></returns>
    [OperationContract]
    bool GetIsCurrentUserAllowPermission(PermissionCode permissionCode);

    /// <summary>
    /// �������� ���������� �� ��������������
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="Permission"/> . 
    /// </returns>
    [OperationContract]
    Permission GetPermission(Guid permissionId);

    /// <summary>
    ///   �������� ������ ���� ����������
    /// </summary>
    /// <param name="roleId"> </param>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Permission> GetPermissions();

    /// <summary>
    /// �������� ������ ���������� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Permission> GetPermissionsByNameContains(string contains);

    /// <summary>
    /// �������� ���� �� ��������������
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="Role"/> . 
    /// </returns>
    [OperationContract]
    Role GetRole(Guid roleId);

    /// <summary>
    /// �������� ������ ���������� ��� ����
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Permission> GetRolePermissions(Guid roleId);

    /// <summary>
    ///   �������� ������ ���� �����
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<Role> GetRoles();

    /// <summary>
    /// �������� ������ ����� ��� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByGroup(Guid groupId);

    /// <summary>
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByNameContains(string contains);

    /// <summary>
    /// �������� ������ ���� �����, ��� ������� ��������� ����������
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<Role> GetRolesByPermission(Guid permissionId);

    /// <summary>
    /// �������� ������ ����� ��� ������������
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
    /// ���������� ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="User"/> . 
    /// </returns>
    [OperationContract]
    User GetUser(Guid userId);

    /// <summary>
    /// ���������� ������������ �� �����
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// User 
    /// </returns>
    [OperationContract]
    User GetUserByName(string name);

    /// <summary>
    /// ���������� ��� ������������ �� email
    /// </summary>
    /// <param name="email">
    /// </param>
    /// <returns>
    /// UserName 
    /// </returns>
    [OperationContract]
    string GetUserNameByEmail(string email);

    /// <summary>
    ///   ������ ���� �������������
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<User> GetUsers();

    /// <summary>
    ///   ������ ������������� ������������� ������� ����� ��� ��� (� ����������� �� ���������� �������� ������������)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    [OperationContract]
    IList<User> GetUsersByCurrent();

    /// <summary>
    /// �������� ������ ���� ������������� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<User> GetUsersByGroup(Guid groupId);

    /// <summary>
    /// �������� ������ ������������� ������ ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    [OperationContract]
    IList<User> GetUsersByNameContains(string contains);

    /// <summary>
    /// ����� �� ������������ ���� �������������� ��� ������ � ������ ����� �� ������� ����� ���� ��������������
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> . 
    /// </returns>
    [OperationContract]
    bool IsUserHasAdminPermissions(User user);

    /// <summary>
    /// ���������� ��� ���������� ������
    /// </summary>
    /// <param name="group">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid SaveGroup(Group @group);

    /// <summary>
    /// ��������� ��� ��������� ����������
    /// </summary>
    /// <param name="permission">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    [OperationContract]
    Guid SavePermission(Permission permission);

    /// <summary>
    /// ��������� ��� ��������� ����
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
    /// ��������� ������ ������������
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
    /// �������� �� ������� ������������ ������� ���������������� �����
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    bool IsCurrentUserAdminTF();
    
    /// <summary>
    /// �������� �� ������� ������������ ������� ���
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    bool IsCurrentUserAdminSmo();

    /// <summary>
    /// �������� �� ������������ ������� ���������������� �����
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    [OperationContract]
    bool IsUserAdminTF(Guid userId);

    /// <summary>
    /// �������� �� ������������ ������� ���
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    [OperationContract]
    bool IsUserAdminSmo(Guid userId);

    #endregion
  }
}