// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasGateInternal.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.services
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.model.interfaces;
  using rt.core.services.aspects;

  #endregion

  /// <summary>
  ///   The kladr gate.
  /// </summary>
  public class FiasGateInternal : InterceptedBase, IAddressService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    public readonly IAddressService Service = new FiasService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Возвращает адресный объект
    /// </summary>
    /// <param name="objectId">
    ///   The object Id.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/> .
    /// </returns>
    public Address GetAddress(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetAddress(objectId));
    }

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
    /// The <see cref="List{IAddress}"/>.
    /// </returns>
    public List<Address> GetAddressList(Guid? parentId, string prefix, KladrLevel? level)
    {
      return InvokeInterceptors(() => Service.GetAddressList(parentId, prefix, level));
    }

    /// <summary>
    /// The get first level by tfoms.
    /// </summary>
    /// <param name="okato">
    /// The okato.
    /// </param>
    /// <returns>
    /// The <see cref="IAddress"/> .
    /// </returns>
    public Address GetFirstLevelByTfoms(string okato)
    {
      return InvokeInterceptors(() => Service.GetFirstLevelByTfoms(okato));
    }

    /// <summary>
    /// The get structure address.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="StructureAddress"/>.
    /// </returns>
    public StructureAddress GetStructureAddress(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetStructureAddress(objectId));
    }

    /// <summary>
    /// The get unstructure address.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string GetUnstructureAddress(Guid objectId)
    {
      return InvokeInterceptors(() => Service.GetUnstructureAddress(objectId));
    }

    /// <summary>
    /// The hierarchy build.
    /// </summary>
    /// <param name="objectId">
    ///   The object id.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public string HierarchyBuild(Guid objectId)
    {
      return InvokeInterceptors(() => Service.HierarchyBuild(objectId));
    }

    #endregion
  }
}