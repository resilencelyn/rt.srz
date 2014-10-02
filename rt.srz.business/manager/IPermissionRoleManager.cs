// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionRoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface PermissionRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface PermissionRoleManager.
  /// </summary>
  public partial interface IPermissionRoleManager
  {
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
    void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions);

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
    void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// �������� ������ ���������� ��� ����
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Permission> GetRolePermissions(Guid roleId);

    /// <summary>
    /// �������� ������ ���� �����, ��� ������� ��������� ����������
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Role> GetRolesByPermission(Guid permissionId);


  }
}