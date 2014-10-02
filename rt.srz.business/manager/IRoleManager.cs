// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface RoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface RoleManager.
  /// </summary>
  public partial interface IRoleManager
  {
    /// <summary>
    /// ���������� ����
    /// </summary>
    /// <param name="name">
    /// The name. 
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    Guid AddRole(string name);

    /// <summary>
    /// �������� ����
    /// </summary>
    /// <param name="id">
    /// </param>
    void DeleteRole(Guid id);

    /// <summary>
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Role> GetRolesByNameContains(string contains);


  }
}