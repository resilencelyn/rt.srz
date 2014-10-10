// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISearchKeyTypeManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface SearchKeyTypeManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager
{
  using System;
  using System.Collections.Generic;

  using rt.srz.model.srz;

  /// <summary>
  ///   The interface SearchKeyTypeManager.
  /// </summary>
  public partial interface ISearchKeyTypeManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Удаление ключа поиска (set пометка IsActive=false)
    /// </summary>
    /// <param name="keyTypeId">
    /// The key Type Id.
    /// </param>
    void DeleteSearchKeyType(Guid keyTypeId);

    /// <summary>
    ///   Возвращает описатели всех ключей поиска для указанного ТФОМС
    /// </summary>
    /// <returns>
    ///   The <see cref="IList{SearchKeyType}" />.
    /// </returns>
    IList<SearchKeyType> GetSearchKeyTypesByTFoms();

    /// <summary>
    /// Сохраняет ключ поиска в БД
    /// </summary>
    /// <param name="keyType">
    /// The key Type.
    /// </param>
    /// <returns>
    /// The <see cref="Guid"/>.
    /// </returns>
    Guid SaveSearchKeyType(SearchKeyType keyType);

    #endregion
  }
}