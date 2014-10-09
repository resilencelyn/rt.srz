// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KladrService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The kladr service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Kladr
{
  #region

  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.ServiceModel;

  using rt.core.services.nhibernate;
  using rt.core.services.wcf;
  using rt.srz.business.manager;
  using rt.srz.model.enumerations;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.srz;

  using StructureMap;

  #endregion

  /// <summary>
  ///   The kladr service.
  /// </summary>
  [NHibernateWcfContext]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [ErrorHandlingBehavior]
  public class KladrService : IKladrService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    public Kladr GetKladr(Guid objectId)
    {
      return ObjectFactory.GetInstance<IKladrManager>().GetById(objectId);
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
    /// The <see cref="IList{Kladr}"/>.
    /// </returns>
    public List<Kladr> GetKladrs(Guid? parentId, string prefix, KladrLevel? level)
    {
      return ObjectFactory.GetInstance<IKladrManager>().GetKladrs(parentId, prefix, level).ToList();
    }

    #endregion
  }
}