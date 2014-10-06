// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Atlantiko
{
  using System.Collections.Generic;
  using System.Linq;

  using Quartz;

  using rt.atl.business.exchange;
  using rt.atl.business.exchange.interfaces;
  using rt.atl.business.manager;
  using rt.atl.model.dto;
  using rt.atl.model.interfaces.Service;
  using rt.core.model.dto;

  using StructureMap;

  /// <summary>
  ///   The atl service.
  /// </summary>
  public class AtlService : IAtlService
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      ObjectFactory.GetInstance<IExchangePvpManager>().FlagExportedPrzBuff();
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
      return ObjectFactory.GetInstance<IPrzbufManager>().GetErrorSinchronizationInfoList(criteria);
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns>
    /// The <see cref="IList"/>.
    /// </returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return ObjectFactory.GetInstance<IpersonManager>().GetStatisticInitialLoading();
    }

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.ExportToPvp).First();
      exporter.Run(context);
    }

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.ExportToSrz).First();
      exporter.Run(context);
    }

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.SinhronizeNsi).First();
      exporter.Run(context);
    }

    #endregion
  }
}