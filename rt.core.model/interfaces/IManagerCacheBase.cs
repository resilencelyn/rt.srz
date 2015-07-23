// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerCacheBase.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ManagerCacheBase interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  /// <summary>
  ///   The ManagerCacheBase interface.
  /// </summary>
  public interface IManagerCacheBase
  {
    #region Public Methods and Operators

    /// <summary>
    ///   Сбросить кэш, чтобы при следующем обращении запрос пошел в базу а не к кэшу.
    /// </summary>
    void Refresh();

    #endregion
  }
}