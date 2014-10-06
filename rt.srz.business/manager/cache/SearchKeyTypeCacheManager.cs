// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchKeyTypeCacheManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The concept cache manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  using System;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  /// <summary>
  ///   The concept cache manager.
  /// </summary>
  public class SearchKeyTypeCacheManager : ManagerCacheBaseT<SearchKeyType, Guid>, ISearchKeyTypeCacheManager
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchKeyTypeCacheManager"/> class.
    /// </summary>
    /// <param name="repository">
    /// The repository.
    /// </param>
    public SearchKeyTypeCacheManager(ISearchKeyTypeManager repository)
      : base(repository)
    {
      TimeSpan = new TimeSpan(0, 0, 30, 0);
      Cache = Repository.GetAll(int.MaxValue);
      TimeQueryDb = DateTime.Now;
    }

    #endregion
  }
}