// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GroupManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The GroupManager.
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
  ///   The GroupManager.
  /// </summary>
  public partial class GroupManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление группы
    /// </summary>
    /// <param name="name">
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/> .
    /// </returns>
    public Guid AddGroup(string name)
    {
      var group = new Group();
      group.Name = name;
      SaveOrUpdate(group);
      return group.Id;
    }

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    public void DeleteGroup(Guid groupId)
    {
      ObjectFactory.GetInstance<IUserGroupRoleManager>().Delete(x => x.Group.Id == groupId);
      ObjectFactory.GetInstance<IUserGroupManager>().Delete(x => x.Group.Id == groupId);
      Delete(x => x.Id == groupId);
      ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession().Flush();
    }

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Group> GetGroupsByNameContains(string contains)
    {
      var session = ObjectFactory.GetInstance<ISessionFactory>().GetCurrentSession();
      return
        session.QueryOver<Group>()
               .WhereRestrictionOn(g => g.Name)
               .IsInsensitiveLike(contains, MatchMode.Anywhere)
               .List();
    }

    #endregion
  }
}