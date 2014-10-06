// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPrzbufManager.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The interface PrzbufManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.business.manager
{
  using System.ServiceModel;

  using rt.atl.model.dto;
  using rt.core.model.dto;

  /// <summary>
  ///   The interface PrzbufManager.
  /// </summary>
  public partial interface IPrzbufManager
  {
    #region Public Methods and Operators

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    [OperationContract]
    SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(
      SearchErrorSinchronizationCriteria criteria);

    #endregion
  }
}