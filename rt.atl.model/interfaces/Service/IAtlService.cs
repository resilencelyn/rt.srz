using rt.atl.model.dto;
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAtlService.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The AtlGate interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
using System.ServiceModel;
using Quartz;
using System.Collections.Generic;
namespace rt.atl.model.interfaces.Service
{
  using rt.core.model.dto;

  /// <summary>
  ///   The AtlGate interface.
  /// </summary>
  [ServiceContract]
  public interface IAtlService
  {
    /// <summary>
    ///   The run export to srz.
    /// </summary>
    [OperationContract]
    void RunExportToSrz(IJobExecutionContext context);

    /// <summary>
    /// The run export to srz.
    /// </summary>
    [OperationContract]
    void RunSinhronizeNsi(IJobExecutionContext context);

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    [OperationContract]
    void RunExportToPvp(IJobExecutionContext context);

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [OperationContract]
    SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(SearchErrorSinchronizationCriteria criteria);

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    IList<StatisticInitialLoading> GetStatisticInitialLoading();

    /// <summary>
    /// The flag exported prz buff.
    /// </summary>
    [OperationContract]
    void FlagExportedPrzBuff();
  }
}