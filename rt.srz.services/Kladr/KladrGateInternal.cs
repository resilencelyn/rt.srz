// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrGateInternal.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The kladr gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Kladr
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.services.aspects;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The kladr gate.
  /// </summary>
  public class KladrGateInternal : InterceptedBase, IKladrService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    private readonly IKladrService Service = new KladrService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Kladr GetKladr(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetKladr(objectId));
    }

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
    /// The <see cref="List{Kladr}"/>.
    /// </returns>
    public List<Kladr> GetKladrs(Guid? parentId, string prefix, KladrLevel? level)
    {
      return InvokeInterceptors(() => Service.GetKladrs(parentId, prefix, level));
    }

    #endregion
  }
}