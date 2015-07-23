// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserManager.cs" company="������">
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
    /// ���������� ������ ������ ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="pdpId">
    /// </param>
    void AssignPdpToUser(Guid userId, Guid? pdpId);

    /// <summary>
    /// �������� ������������
    /// </summary>
    /// <param name="userId">
    /// </param>
    void DeleteUser(Guid userId);

    /// <summary>
    /// ��������� �� ������������ ���������� � ��������� �����
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
    ///   ������ ���� �������������
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsers();

    /// <summary>
    /// �������� ������ ������������� ������ ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<User> GetUsersByNameContains(string contains);

    /// <summary>
    /// ����� �� ������������ ���� �������������� ��� ������ � ������ ����� �� ������� ����� ���� ��������������
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
    /// �������� �� ������������ ������� ���
    /// </summary>
    /// <param name="userId">
    /// The user Id.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IsUserAdminSmo(Guid userId);

    /// <summary>
    /// �������� �� ������������ ������� ���������������� �����
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