// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchKeyTypeCacheManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ConceptCacheManager interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.cache
{
  using System;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  /// <summary>
  ///   The ConceptCacheManager interface.
  /// </summary>
  public interface ISearchKeyTypeCacheManager : IManagerCacheBaseT<SearchKeyType, Guid>
  {
  }
}