//-----------------------------------------------------------------------
// <copyright file="AtlGate.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------       

namespace rt.srz.services.Atlantiko
{
  using System.Collections.Generic;

  using Quartz;

  using rt.atl.model.dto;
  using rt.atl.model.interfaces.Service;
  using rt.core.model.dto;
  using rt.core.services.aspects;

  /// <summary>
  /// The atl service.
  /// </summary>
  public class AtlGate : InterceptedBase, IAtlService
  {
    /// <summary>
    /// The services.
    /// </summary>
    private readonly IAtlService services = new AtlService();

    /// <summary>
    /// The run export to srz.
    /// </summary>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunExportToSrz(context));
    }

    /// <summary>
    /// The run sinhronize nsi.
    /// </summary>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunSinhronizeNsi(context));
    }

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunExportToPvp(context));
    }

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(SearchErrorSinchronizationCriteria criteria)
    {
      return InvokeInterceptors(() => services.GetErrorSinchronizationInfoList(criteria));
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return InvokeInterceptors(() => services.GetStatisticInitialLoading());
    }

    /// <summary>
    /// The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      InvokeInterceptors(() => services.FlagExportedPrzBuff());
    }
  }
}
