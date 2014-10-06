// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client
{
  #region references

  using System.Collections.Generic;

  using Quartz;

  using rt.atl.model.dto;
  using rt.atl.model.interfaces.Service;
  using rt.core.model.dto;
  using rt.core.services.registry;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class AtlClient : ServiceClient<IAtlService>, IAtlService
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      InvokeInterceptors(() => Service.FlagExportedPrzBuff());
    }

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria">
    /// </param>
    /// <returns>
    /// The <see cref="SearchResult"/>.
    /// </returns>
    public SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(
      SearchErrorSinchronizationCriteria criteria)
    {
      return InvokeInterceptors(() => Service.GetErrorSinchronizationInfoList(criteria));
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return InvokeInterceptors(() => Service.GetStatisticInitialLoading());
    }

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunExportToPvp(context));
    }

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunExportToSrz(context));
    }

    /// <summary>
    /// The run sinhronize nsi.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      InvokeInterceptors(() => Service.RunSinhronizeNsi(context));
    }

    #endregion
  }
}