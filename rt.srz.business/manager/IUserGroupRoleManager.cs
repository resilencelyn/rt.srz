// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserGroupRoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface UserGroupRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface UserGroupRoleManager.
  /// </summary>
  public partial interface IUserGroupRoleManager
  {
    /// <summary>
    /// ���������� ������������ �����
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// ���������� ������������ �����
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles);

    /// <summary>
    /// �������� ������ ����� ��� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Role> GetRolesByGroup(Guid groupId);

    /// <summary>
    /// �������� ������ ����� ��� ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Role> GetRolesByUser(Guid userId);

  }
}