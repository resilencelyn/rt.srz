// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGroupManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface GroupManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.core.model.core;

  /// <summary>
  ///   The interface GroupManager.
  /// </summary>
  public partial interface IGroupManager
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
    Guid AddGroup(string name);

    /// <summary>
    /// Удаление группы
    /// </summary>
    /// <param name="groupId">
    /// </param>
    void DeleteGroup(Guid groupId);

    /// <summary>
    /// Получает список групп названия которых начинаются с указанного значения
    /// </summary>
    /// <param name="contains">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    IList<Group> GetGroupsByNameContains(string contains);

    #endregion
  }
}