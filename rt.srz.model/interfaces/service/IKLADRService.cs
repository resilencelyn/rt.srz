// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKLADRService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    /// The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/>.
    /// </returns>
    [OperationContract]
    Kladr GetKladr(Guid objectId);

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
    /// The <see cref="List{Kladr}"/> .
    /// </returns>
    [OperationContract]
    List<Kladr> GetKladrs(Guid? parentId, string prefix, KladrLevel? level);

    #endregion
  }
}