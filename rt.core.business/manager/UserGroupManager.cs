// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGroupManager.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The UserGroupManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  using rt.core.model.core;

  /// <summary>
  ///   The UserGroupManager.
  /// </summary>
  public partial class UserGroupManager
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
    public void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups)
    {
      foreach (var groupId in assignGroups)
      {
        var ug = GetBy(x => x.User.Id == userId && x.Group.Id == groupId).FirstOrDefault();
        if (ug == null)
        {
          ug = new UserGroup();
          ug.User = new User();
          ug.Group = new Group();
          ug.User.Id = userId;
          ug.Group.Id = groupId;
          SaveOrUpdate(ug);
        }
      }

      foreach (var id in detachGroups)
      {
        Delete(x => x.User.Id == userId && x.Group.Id == id);
      }
    }

    /// <summary>
    /// Добавление пользователей в группу, удаление пользователей из группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignUsers">
    /// </param>
    /// <param name="detachUsers">
    /// </param>
    public void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers)
    {
      foreach (var userId in assignUsers)
      {
        var ug = GetBy(x => x.User.Id == userId && x.Group.Id == groupId).FirstOrDefault();
        if (ug == null)
        {
          ug = new UserGroup();
          ug.User = new User();
          ug.Group = new Group();
          ug.User.Id = userId;
          ug.Group.Id = groupId;
          SaveOrUpdate(ug);
        }
      }

      foreach (var id in detachUsers)
      {
        Delete(x => x.User.Id == id && x.Group.Id == groupId);
      }
    }

    /// <summary>
    /// Получает список всех групп, куда входит данный пользователь
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Group> GetGroupsByUser(Guid userId)
    {
      return GetBy(ug => ug.User.Id == userId).Select(x => x.Group).ToList();
    }

    /// <summary>
    /// Получает список всех пользователей группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<User> GetUsersByGroup(Guid groupId)
    {
      return GetBy(ug => ug.Group.Id == groupId).Select(x => x.User).Where(x => x.IsApproved).ToList();
    }

    #endregion
  }
}