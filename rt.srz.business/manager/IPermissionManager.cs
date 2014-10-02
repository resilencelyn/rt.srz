// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface PermissionManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface PermissionManager.
  /// </summary>
  public partial interface IPermissionManager
  {
    /// <summary>
    /// ƒобавление разрешени€
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
    /// ”даление разрешени€
    /// </summary>
    /// <param name="id">
    /// </param>
    void DeletePermission(Guid id);

    /// <summary>
    /// ѕровер€ет есть ли в базе разрешение с указанным ид и кодом
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
    /// ѕолучает список разрешений названи€ которых начинаютс€ с указанного значени€
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Permission> GetPermissionsByNameContains(string contains);


  }
}