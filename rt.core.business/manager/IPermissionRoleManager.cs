// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionRoleManager.cs" company="РусБИТех">
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
    void AssignPermissionsToRole(Guid roleId, List<Guid> assignPermissions, List<Guid> detachPermissions);

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
    void AssignRolesToPermission(Guid permissionId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// Получает список разрешений для роли
    /// </summary>
    /// <param name="roleId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Permission> GetRolePermissions(Guid roleId);

    /// <summary>
    /// Получает список всех ролей, для которых назначено разрешение
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