// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserGroupManager.cs" company="��������">
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
    /// ���������� ������������ � ������, �������� �� �����
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignGroups">
    /// ������ � ������� ����������� ������������
    /// </param>
    /// <param name="detachGroups">
    /// ������ �� ������� ����������� ������������
    /// </param>
    void AssignGroupsToUser(Guid userId, List<Guid> assignGroups, List<Guid> detachGroups);

    /// <summary>
    /// ���������� ������������� � ������, �������� ������������� �� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignUsers">
    /// </param>
    /// <param name="detachUsers">
    /// </param>
    void AssignUsersToGroup(Guid groupId, List<Guid> assignUsers, List<Guid> detachUsers);

    /// <summary>
    /// �������� ������ ���� �����, ���� ������ ������ ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Group> GetGroupsByUser(Guid userId);

    /// <summary>
    /// �������� ������ ���� ������������� ������
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