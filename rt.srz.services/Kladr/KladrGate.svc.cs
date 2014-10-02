// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The kladr gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.ServiceModel;
using rt.core.services;
using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.registry;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;

#endregion

namespace rt.srz.services.KLADR
{
  using rt.core.services.wcf;

  /// <summary>
  /// The kladr gate.
  /// </summary>
  [NHibernateWcfContext]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [ErrorHandlingBehavior]
  public class KladrGate : InterceptedBase, IKladrService
  {
    private readonly IKladrService Service = new KladrService();

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    /// </param>
    /// <param name="prefix">
    ///   The prefix.
    /// </param>
    /// <param name="level">
    /// </param>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<Kladr> GetKLADRs(Guid? parentId, string prefix, KLADRLevel? level)
    {
      return InvokeInterceptors(() => Service.GetKLADRs(parentId, prefix, level));
    }

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetKLADR(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetKLADR(objectId));
    }

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetKLADRByCode(string code)
    {
      return InvokeInterceptors(() => Service.GetKLADRByCode(code));
    }

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="tfom">
    /// The tfom.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetFirstLevelByTfoms(Organisation tfom)
    {
      return InvokeInterceptors(() => Service.GetFirstLevelByTfoms(tfom));
    }
  }
}