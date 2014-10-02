//-----------------------------------------------------------------------
// <copyright file="AtlService.cs" company="Rintech">
//     Copyright (c) 2013. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------     

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
  /// The atl service.
  /// </summary>
  public class AtlService : IAtlService
  {
    /// <summary>
    /// The run export to srz.
    /// </summary>
    public void RunExportToSrz(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.ExportToSrz).First();
      exporter.Run(context);
    }

    /// <summary>
    /// The run export to srz.
    /// </summary>
    public void RunSinhronizeNsi(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.SinhronizeNsi).First();
      exporter.Run(context);
    }

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    public void RunExportToPvp(IJobExecutionContext context)
    {
      var exporter = ObjectFactory.GetInstance<IExchangeFactory>().GetExporter(ExchangeTypeEnum.ExportToPvp).First();
      exporter.Run(context);
    }

    /// <summary>
    /// Получает список ошибок синхронизации
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public SearchResult<ErrorSinchronizationInfoResult> GetErrorSinchronizationInfoList(SearchErrorSinchronizationCriteria criteria)
    {
      return ObjectFactory.GetInstance<IPrzbufManager>().GetErrorSinchronizationInfoList(criteria);
    }

    /// <summary>
    /// Статистика первичной загрузки
    /// </summary>
    /// <returns></returns>
    public IList<StatisticInitialLoading> GetStatisticInitialLoading()
    {
      return ObjectFactory.GetInstance<IpersonManager>().GetStatisticInitialLoading();
    }

    /// <summary>
    /// The flag exported prz buff.
    /// </summary>
    public void FlagExportedPrzBuff()
    {
      ObjectFactory.GetInstance<IExchangePvpManager>().FlagExportedPrzBuff();
    }
  }
}
