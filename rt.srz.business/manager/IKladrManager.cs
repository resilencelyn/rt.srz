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
    /// <param name="tfom">
    /// The tfom.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    Kladr GetFirstLevelByTfoms(Organisation tfom);

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectID">
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    Kladr GetKLADR(Guid objectID);

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    Kladr GetKLADRByCode(string code);

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentID">
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// </param>
    /// <returns>
    /// The <see cref="IList{T}"/>.
    /// </returns>
    IList<Kladr> GetKLADRs(Guid? parentID, string prefix, KLADRLevel? level);

    #endregion
  }
}