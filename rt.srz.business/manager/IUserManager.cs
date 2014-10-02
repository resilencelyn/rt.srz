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
    ///   ������ ���� �������������
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsers();


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
    /// �������� �� ������������ ������� ���
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    bool IsUserAdminSmo(Guid userId);

    /// <summary>
    /// �������� �� ������������ ������� ���������������� �����
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns></returns>
    bool IsUserAdminTF(Guid userId);

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
    ///   ������ ������������� ������������� ������� ����� ��� ��� (� ����������� �� ���������� �������� ������������)
    /// </summary>
    /// <returns> The <see cref="IList" /> . </returns>
    IList<User> GetUsersByCurrent();

    /// <summary>
    /// �������� ������ ������������� ������ ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<User> GetUsersByNameContains(string contains);


  }
}