// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKladrManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface KladrManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  #region references

  using System;
  using System.Collections.Generic;

  using rt.core.model.interfaces;
  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The interface KladrManager.
  /// </summary>
  public partial interface IKladrManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    IAddress GetFirstLevelByTfoms(string okato);

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    /// The parent Id.
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// The level.
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    IList<Kladr> GetKladrs(Guid? parentId, string prefix, KladrLevel? level);

    #endregion

    /// <summary>
    /// The get structure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="StructureAddress"/>.
    /// </returns>
    StructureAddress GetStructureAddress(Guid objectId);

    /// <summary>
    /// The get unstructure address.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string GetUnstructureAddress(Guid objectId);

    /// <summary>
    /// The hierarchy build.
    /// </summary>
    /// <param name="objectId">
    /// The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string HierarchyBuild(Guid objectId);
  }
}