// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The atl client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client
{
  #region references

  using Quartz;
  using rt.atl.model.dto;
  using rt.atl.model.interfaces.Service;
  using rt.core.model.dto;
  using rt.core.services.registry;
  using System.Collections.Generic;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class AtlClient : ServiceClient<IAtlService>, IAtlService
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The run export to pvp.
    /// </summary>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunExportToPvp(context));
    }

    /// <summary>
    ///   The run export to srz.
    /// </summary>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunExportToSrz(context));
    }

    /// <summary>
    ///   The run sinhronize nsi.
    /// </summary>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunSinhronizeNsi(context));
    }

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(SearchErrorSinchronizationCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetErrorSinchronizationInfoList(criteria));
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return InvokeInterceptors(() => Service.GetStatisticInitialLoading());
    }

    /// <summary>
    /// The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      InvokeInterceptors(() => Service.FlagExportedPrzBuff());
    }

    #endregion
  }
}