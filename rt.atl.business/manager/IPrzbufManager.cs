// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPrzbufManager.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The interface PrzbufManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using rt.atl.model.dto;

using System.ServiceModel;
namespace rt.atl.business.manager
{
  using rt.core.model.dto;

  /// <summary>
  ///   The interface PrzbufManager.
  /// </summary>
  public partial interface IPrzbufManager
  {
    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [OperationContract]
    SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(SearchErrorSinchronizationCriteria criteria);
  }
}