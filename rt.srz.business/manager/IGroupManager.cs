// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGroupManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface GroupManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Collections.Generic;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The interface GroupManager.
  /// </summary>
  public partial interface IGroupManager
  {
    /// <summary>
    /// ���������� ������
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> . 
    /// </returns>
    Guid AddGroup(string name);

    /// <summary>
    /// �������� ������
    /// </summary>
    /// <param name="groupId">
    /// </param>
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// �������� ������ ����� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    IList<Group> GetGroupsByNameContains(string contains);


  }
}