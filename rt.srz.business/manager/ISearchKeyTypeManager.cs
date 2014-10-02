//-------------------------------------------------------------------------------------
// <copyright file="ISearchKeyTypeManager.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using rt.srz.model.srz;

namespace rt.srz.business.manager
{
  /// <summary>
  /// The interface SearchKeyTypeManager.
  /// </summary>
  public partial interface ISearchKeyTypeManager
  {
    /// <summary>
    /// Возвращает описатели всех ключей поиска для указанного ТФОМС 
    /// </summary>
    /// <returns></returns>
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();
    
    /// <summary>
    /// Сохраняет ключ поиска в БД
    /// </summary>
    /// <param name="keyType"></param>
    /// <returns></returns>
    Guid SaveSearchKeyType(SearchKeyType keyType);

    /// <summary>
    /// Удаление ключа поиска (set пометка IsActive=false)
    /// </summary>
    /// <param name="pdpId">
    /// </param>
    void DeleteSearchKeyType(Guid keyTypeId);
  }
}