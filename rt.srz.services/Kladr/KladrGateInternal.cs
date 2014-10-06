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
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="tfom">
    /// The tfom.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Kladr GetFirstLevelByTfoms(Organisation tfom)
    {
      return InvokeInterceptors(() => Service.GetFirstLevelByTfoms(tfom));
    }

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Kladr GetKLADR(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetKLADR(objectId));
    }

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    /// </param>
    /// <param name="prefix">
    /// The prefix.
    /// </param>
    /// <param name="level">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/> .
    /// </returns>
    public IList<Kladr> GetKLADRs(Guid? parentId, string prefix, KLADRLevel? level)
    {
      return InvokeInterceptors(() => Service.GetKLADRs(parentId, prefix, level));
    }

    #endregion
  }
}