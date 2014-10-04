// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionRoleManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface PermissionRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface PermissionRoleManager.
  /// </summary>
  public partial interface IPermissionRoleManager
  {
    #region Public Methods and Operators

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

    #endregion
  }
}