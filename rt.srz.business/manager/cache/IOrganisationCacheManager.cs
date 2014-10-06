// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrganisationCacheManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The OrganisationCacheManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  #region references

  using System;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  #endregion

  /// <summary>
  ///   The OrganisationCacheManager interface.
  /// </summary>
  public interface IOrganisationCacheManager : IManagerCacheBaseT<Organisation, Guid>
  {
  }
}