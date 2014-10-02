// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionRoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The PermissionRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Linq;
using System.Collections.Generic;
using StructureMap;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The PermissionRoleManager.
  /// </summary>
  public partial class PermissionRoleManager
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
    public void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions)
    {
      foreach (var permissionid in assignPermissions)
      {
        var prole = GetBy(pp => pp.Permission.Id == permissionid && pp.Role.Id == roleId).FirstOrDefault();
        if (prole == null)
        {
          prole = new PermissionRole();
          prole.Role = new Role();
          prole.Permission = new Permission();
          prole.Role.Id = roleId;
          prole.Permission.Id = permissionid;
          SaveOrUpdate(prole);
        }
      }

      foreach (var id in detachPermissions)
      {
        Delete(pr => pr.Role.Id == roleId && pr.Permission.Id == id);
      }
    }

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
    public void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      foreach (var roleId in assignRoles)
      {
        var prole = GetBy(pp => pp.Permission.Id == permissionId && pp.Role.Id == roleId).FirstOrDefault();
        if (prole == null)
        {
          prole = new PermissionRole();
          prole.Role = new Role();
          prole.Permission = new Permission();
          prole.Role.Id = roleId;
          prole.Permission.Id = permissionId;
          SaveOrUpdate(prole);
        }
      }

      foreach (var id in detachRoles)
      {
        Delete(pr => pr.Role.Id == id && pr.Permission.Id == permissionId);
      }
    }

    /// <summary>
    /// �������� ������ ���������� ��� ����
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Permission> GetRolePermissions(Guid roleId)
    {
      return GetBy(pr => pr.Role.Id == roleId).Select(pr => pr.Permission).ToList();
    }

    /// <summary>
    /// �������� ������ ���� �����, ��� ������� ��������� ����������
    /// </summary>
    /// <param name="permissionId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByPermission(Guid permissionId)
    {
      return GetBy(pr => pr.Permission.Id == permissionId).Select(pr => pr.Role).ToList();
    }

  }
}