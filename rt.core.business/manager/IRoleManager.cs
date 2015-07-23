// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface RoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface RoleManager.
  /// </summary>
  public partial interface IRoleManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление роли
    /// </summary>
    /// <param name="id">
    /// </param>
    void DeleteRole(Guid id);

    /// <summary>
    /// Получает список ролей названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Role> GetRolesByNameContains(string contains);

    #endregion
  }
}