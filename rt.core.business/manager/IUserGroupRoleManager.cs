// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserGroupRoleManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface UserGroupRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface UserGroupRoleManager.
  /// </summary>
  public partial interface IUserGroupRoleManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Role> GetRolesByGroup(Guid groupId);

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Role> GetRolesByUser(Guid userId);

    #endregion
  }
}