// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKLADRService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The KLADRService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  using rt.srz.model.enumerations;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The KLADRService interface.
  /// </summary>
  [ServiceContract]
  public interface IKladrService
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
    [OperationContract]
    Kladr GetFirstLevelByTfoms(Organisation tfom);

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> . 
    /// </returns>
    [OperationContract]
    Kladr GetKLADR(Guid objectId);

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
    [OperationContract]
    IList<Kladr> GetKLADRs(Guid? parentId, string prefix, KLADRLevel? level);

    #endregion
  }
}