// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserGroupRoleManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The UserGroupRoleManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.srz.model.srz;
using System;
using System.Linq;
using System.Collections.Generic;
using StructureMap;
namespace rt.srz.business.manager
{
  /// <summary>
  ///   The UserGroupRoleManager.
  /// </summary>
  public partial class UserGroupRoleManager
  {
    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    public void AssignRolesToGroup(Guid groupId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      foreach (var roleId in assignRoles)
      {
        var ugrole = GetBy(x => x.Role.Id == roleId && x.Group.Id == groupId).FirstOrDefault();
        if (ugrole == null)
        {
          ugrole = new UserGroupRole();
          ugrole.Role = new Role();
          ugrole.Group = new Group();
          ugrole.Role.Id = roleId;
          ugrole.Group.Id = groupId;
          SaveOrUpdate(ugrole);
        }
      }

      foreach (var id in detachRoles)
      {
        Delete(x => x.Role.Id == id && x.Group.Id == groupId);
      }
    }

    /// <summary>
    /// Назначение пользователю ролей
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <param name="assignRoles">
    /// </param>
    /// <param name="detachRoles">
    /// </param>
    public void AssignRolesToUser(Guid userId, List<Guid> assignRoles, List<Guid> detachRoles)
    {
      foreach (var roleId in assignRoles)
      {
        var ugrole = GetBy(x => x.Role.Id == roleId && x.User.Id == userId).FirstOrDefault();
        if (ugrole == null)
        {
          ugrole = new UserGroupRole();
          ugrole.Role = new Role();
          ugrole.User = new User();
          ugrole.Role.Id = roleId;
          ugrole.User.Id = userId;
          SaveOrUpdate(ugrole);
        }
      }

      foreach (var id in detachRoles)
      {
        Delete(x => x.Role.Id == id && x.User.Id == userId);
      }
    }

    /// <summary>
    /// Получает список ролей для группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByGroup(Guid groupId)
    {
      return GetBy(x => x.Group.Id == groupId).Select(x => x.Role).ToList();
    }

    /// <summary>
    /// Получает список ролей для пользователя
    /// </summary>
    /// <param name="userId">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> . 
    /// </returns>
    public IList<Role> GetRolesByUser(Guid userId)
    {
      return GetBy(x => x.User.Id == userId).Select(x => x.Role).ToList();
    }

  }
}