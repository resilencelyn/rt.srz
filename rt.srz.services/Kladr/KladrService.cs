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
      return ObjectFactory.GetInstance<IKladrManager>().GetFirstLevelByTfoms(tfom);
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
      return ObjectFactory.GetInstance<IKladrManager>().GetKLADR(objectId);
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
      return ObjectFactory.GetInstance<IKladrManager>().GetKLADRs(parentId, prefix, level);
    }

    #endregion
  }
}