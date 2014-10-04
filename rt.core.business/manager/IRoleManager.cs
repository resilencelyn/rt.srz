// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRoleManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface RoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface RoleManager.
  /// </summary>
  public partial interface IRoleManager
  {
    #region Public Methods and Operators

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

    #endregion
  }
}