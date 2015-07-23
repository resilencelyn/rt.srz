// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface UserManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface UserManager.
  /// </summary>
  public partial interface IUserManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Назначение пункта выдачи пользователю
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="pdpId">
    /// </param>
    void AssignPdpToUser(Guid userId, Guid? pdpId);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    void DeleteUser(Guid userId);

    /// <summary>
    /// Разрешено ли пользователю разрешение с указанным кодом
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="permissionCode">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    bool GetIsUserAllowPermission(Guid userId, int permissionCode);

    /// <summary>
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsers();

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<User> GetUsersByNameContains(string contains);

    /// <summary>
    /// Имеет ли пользователь роль администратора или входит в группы любая из которых имеет роль администратора
    /// </summary>
    /// <param name="user">
    /// </param>
    /// <returns>
    /// The <see cref="bool"/> .
    /// </returns>
    bool IsUserHasAdminPermissions(User user);

    /// <summary>
    /// The mark as deleted.
    /// </summary>
    /// <param name="userId">
    /// The user id.
    /// </param>
    void MarkAsDeleted(Guid userId);

    #endregion

    /// <summary>
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IsUserAdminSmo(Guid userId);

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IsUserAdminTf(Guid userId);
  }
}