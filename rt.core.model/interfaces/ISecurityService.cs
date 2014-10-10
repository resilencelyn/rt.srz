// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityService.cs" company="��������">
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
    /// ��������� ������������
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
    /// ���������� ������������ � ������, �������� �� �����
    /// </summary>
    /// <param name="userId">
    /// The user Id.
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
    /// The user Id.
    /// </param>
    /// <param name="pdpId">
    /// The pdp Id.
    /// </param>
    [OperationContract]
    void AssignPdpToUser(Guid userId, Guid? pdpId);

    /// <summary>
    /// ��������� (����������, ������������) ���������� ��� ����
    /// </summary>
    /// <param name="roleId">
    /// The role Id.
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
    /// ����������, ������������ ����� ��� ����������
    /// </summary>
    /// <param name="permissionId">
    /// The permission Id.
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
    /// ���������� ������������� � ������, �������� ������������� �� ������
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
    /// �������� ������
    /// </summary>
    /// <param name="groupId">
    /// The group Id.
    /// </param>
    [OperationContract]
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// �������� ����������
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeletePermission(Guid id);

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="id">
    /// The id.
    /// </param>
    [OperationContract]
    void DeleteRole(Guid id);

    /// <summary>
    /// �������� ������������
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    [OperationContract]
    void DeleteUser(Guid userId);

    /// <summary>
    /// ��������� ���� �� � ���� ���������� � ��������� �� � �����
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
    ///   ���������� �������� ������������
    /// </summary>
    /// <returns> The <see cref="User" /> . </returns>
    [OperationContract]
    User GetCurrentUser();

    /// <summary>
    /// �������� ������ �� ��������������
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
    ///   ������ ���� �����
    /// </summary>
    /// <returns> The <see cref="List{Group}" /> . </returns>
    [OperationContract]
    List<Group> GetGroups();

    /// <summary>
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
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
    /// �������� ������ ���� �����, ���� ������ ������ ������������
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
    /// ��������� �� �������� ������������ ����������
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
    /// ��������� �� ������������ ���������� � ��������� �����
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
    /// �������� ���������� �� ��������������
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
    ///   �������� ������ ���� ����������
    /// </summary>
    /// <returns> The <see cref="List{Permission}" /> . </returns>
    [OperationContract]
    List<Permission> GetPermissions();

    /// <summary>
    /// �������� ������ ���������� �������� ������� ���������� � ���������� ��������
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
    /// �������� ���� �� ��������������
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
    /// �������� ������ ���������� ��� ����
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
    ///   �������� ������ �����
    /// </summary>
    /// <returns> The <see cref="List{Role}" /> . </returns>
    [OperationContract]
    List<Role> GetRoles();

    /// <summary>
    /// �������� ������ ����� ��� ������
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
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
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
    /// �������� ������ ���� �����, ��� ������� ��������� ����������
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
    /// �������� ������ ����� ��� ������������
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
    /// ���������� ������������
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
    /// ���������� ������������ �� �����
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
    /// ���������� ��� ������������ �� email
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
    ///   ������ ���� �������������
    /// </summary>
    /// <returns> The <see cref="List{User}" /> . </returns>
    [OperationContract]
    List<User> GetUsers();

    /// <summary>
    /// �������� ������ ���� ������������� ������
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
    /// �������� ������ ������������� ������ ������� ���������� � ���������� ��������
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
    /// �������� �� ������������ ������� ���
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
    /// �������� �� ������������ ������� ���������������� �����
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
    /// ����� �� ������������ ���� �������������� ��� ������ � ������ ����� �� ������� ����� ���� ��������������
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
    /// ���������� ��� ���������� ������
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
    /// ��������� ��� ��������� ����������
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
    /// ��������� ��� ��������� ����
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
    /// ��������� ��� ��������� ������������
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
    /// ��������� ������ ������������
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