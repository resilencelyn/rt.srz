namespace rt.srz.business.manager.cache
{
  using System;
  using System.Collections.Generic;

  using rt.core.business.nhibernate;
  using rt.srz.model.srz;

  /// <summary>
  ///   The ConceptCacheManager interface.
  /// </summary>
  public interface ISearchKeyTypeCacheManager : IManagerCacheBaseT<SearchKeyType, Guid>
  {
  }
}