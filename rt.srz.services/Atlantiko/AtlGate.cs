// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlGate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Atlantiko
{
  using System.Collections.Generic;

  using Quartz;

  using rt.atl.model.dto;
  using rt.atl.model.interfaces.Service;
  using rt.core.model.dto;
  using rt.core.services.aspects;

  /// <summary>
  ///   The atl service.
  /// </summary>
  public class AtlGate : InterceptedBase, IAtlService
  {
    #region Fields

    /// <summary>
    ///   The services.
    /// </summary>
    private readonly IAtlService services = new AtlService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      InvokeInterceptors(() => services.FlagExportedPrzBuff());
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
      return InvokeInterceptors(() => services.GetErrorSinchronizationInfoList(criteria));
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return InvokeInterceptors(() => services.GetStatisticInitialLoading());
    }

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunExportToPvp(context));
    }

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunExportToSrz(context));
    }

    /// <summary>
    /// The run sinhronize nsi.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      InvokeInterceptors(() => services.RunSinhronizeNsi(context));
    }

    #endregion
  }
}