// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface PermissionManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface PermissionManager.
  /// </summary>
  public partial interface IPermissionManager
  {
    #region Public Methods and Operators

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
    Guid AddPermission(int code, string name);

    /// <summary>
    /// Удаление разрешения
    /// </summary>
    /// <param name="id">
    /// </param>
    void DeletePermission(Guid id);

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
    bool ExistsPermissionCode(Guid permissionId, int code);

    /// <summary>
    /// Получает список разрешений названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Permission> GetPermissionsByNameContains(string contains);

    #endregion
  }
}