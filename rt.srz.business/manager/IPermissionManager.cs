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
    /// ���������� ����������
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
    /// �������� ����������
    /// </summary>
    /// <param name="id">
    /// </param>
    void DeletePermission(Guid id);

    /// <summary>
    /// ��������� ���� �� � ���� ���������� � ��������� �� � �����
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
    /// �������� ������ ���������� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Permission> GetPermissionsByNameContains(string contains);


  }
}