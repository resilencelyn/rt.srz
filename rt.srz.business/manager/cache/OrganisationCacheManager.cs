// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrganisationCacheManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The smo cache manager.
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
  ///   The smo cache manager.
  /// </summary>
  public class OrganisationCacheManager : ManagerCacheBaseT<Organisation, Guid>, IOrganisationCacheManager
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="OrganisationCacheManager"/> class.
    /// </summary>
    /// <param name="repository">
    /// The repository.
    /// </param>
    public OrganisationCacheManager(IOrganisationManager repository)
      : base(repository)
    {
    }

    #endregion
  }
}