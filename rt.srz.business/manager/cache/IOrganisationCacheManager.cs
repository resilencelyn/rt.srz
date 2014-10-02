// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrganisationCacheManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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