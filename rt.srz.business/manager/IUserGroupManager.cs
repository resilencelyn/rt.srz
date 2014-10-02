// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserGroupManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface UserGroupManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface UserGroupManager.
  /// </summary>
  public partial interface IUserGroupManager
  {
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

  }
}