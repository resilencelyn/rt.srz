// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManagerCacheBase.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model
{
  /// <summary>
  /// The ManagerCacheBase interface.
  /// </summary>
  public interface IManagerCacheBase
  {
    #region Public Methods and Operators

    /// <summary>
    ///   —бросить кэш, чтобы при следующем обращении запрос пошел в базу а не к кэшу.
    /// </summary>
    void Refresh();

    #endregion
  }
}