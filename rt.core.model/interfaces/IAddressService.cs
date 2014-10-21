// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddressService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The KLADRService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  #region

  using System;
  using System.Collections.Generic;
  using System.ServiceModel;

  #endregion

  /// <summary>
  ///   The KLADRService interface.
  /// </summary>
  [ServiceContract]
  public interface IAddressService
  {
    #region Public Methods and Operators

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    ///   The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/>.
    /// </returns>
    [OperationContract]
    Address GetAddress(Guid objectId);

    /// <summary>
    /// Возвращает список адресных объектов для указанного уровня
    /// </summary>
    /// <param name="parentId">
    ///   The parent Id.
    /// </param>
    /// <param name="prefix">
    ///   The prefix.
    /// </param>
    /// <param name="level">
    ///   The level.
    /// </param>
    /// <returns>
    /// The <see cref="List{IAddress}"/> .
    /// </returns>
    [OperationContract]
    List<Address> GetAddressList(Guid? parentId, string prefix, KladrLevel? level);

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="okato">
    ///   The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/> .
    /// </returns>
    [OperationContract]
    Address GetFirstLevelByTfoms(string okato);

    /// <summary>
    /// The get structure address.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="StructureAddress"/>.
    /// </returns>
    [OperationContract]
    StructureAddress GetStructureAddress(Guid objectId);

    /// <summary>
    /// The get unstructure address.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [OperationContract]
    string GetUnstructureAddress(Guid objectId);

    /// <summary>
    /// The hierarchy build.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    [OperationContract]
    string HierarchyBuild(Guid objectId);

    #endregion
  }
}