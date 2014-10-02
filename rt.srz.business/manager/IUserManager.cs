// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface UserManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface UserManager.
  /// </summary>
  public partial interface IUserManager
  {
    void MarkAsDeleted(Guid userId);

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
    ///   Список всех пользователей
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsers();


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
    /// Является ли пользователь админом СМО
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    bool IsUserAdminSmo(Guid userId);

    /// <summary>
    /// Является ли пользователь админом территориального фонда
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    bool IsUserAdminTF(Guid userId);

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
    ///   Список пользователей принадлежащих данному фонду или смо (в зависимости от разрешений текущего пользователя)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsersByCurrent();

    /// <summary>
    /// Получает список пользователей логины которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<User> GetUsersByNameContains(string contains);


  }
}