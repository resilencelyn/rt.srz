// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionManager.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The PermissionManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using NHibernate;
  using NHibernate.Criterion;

  using rt.core.model.core;

  using StructureMap;

  /// <summary>
  ///   The PermissionManager.
  /// </summary>
  public partial class PermissionManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// �������� ����������
    /// </summary>
    /// <param name="id">
    /// </param>
    public void DeletePermission(Guid id)
    {
      ObjectFactory.GetInstance<IPermissionRoleManager>().Delete(pr => pr.Permission.Id == id);
      Delete(p => p.Id == id);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

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
    public bool ExistsPermissionCode(Guid permissionId, int code)
    {
      var list = GetBy(p => p.Id != permissionId && p.Code == code);
      return list.Count > 0;
    }

    /// <summary>
    /// �������� ������ ���������� �������� ������� ���������� � ���������� ��������
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Permission> GetPermissionsByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<Permission>()
               .WhereRestrictionOn(g => g.Name)
               .IsInsensitiveLike(contains, MatchMode.Anywhere)
               .List();
    }

    #endregion
  }
}