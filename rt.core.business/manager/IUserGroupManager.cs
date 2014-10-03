// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserGroupManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface UserGroupManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface UserGroupManager.
  /// </summary>
  public partial interface IUserGroupManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление пользователя в группы, удаление из групп
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignGroups">
    /// Группы в которые добавляется пользователь
    /// </param>
    /// <param name="detachGroups">
    /// Группы из которых исключается пользователь
    /// </param>
    void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups);

    /// <summary>
    /// Добавление пользователей в группу, удаление пользователей из группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignUsers">
    /// </param>
    /// <param name="detachUsers">
    /// </param>
    void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers);

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Group> GetGroupsByUser(Guid userId);

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<User> GetUsersByGroup(Guid groupId);

    #endregion
  }
}