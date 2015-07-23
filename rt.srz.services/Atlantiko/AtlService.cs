// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Atlantiko
{
  using System.Linq;

  using Quartz;

  using rt.atl.business.exchange;
  using rt.atl.business.exchange.interfaces;
  using rt.atl.model.interfaces.Service;

  using StructureMap;

  /// <summary>
  ///   The atl service.
  /// </summary>
  public class AtlService : IAtlService
  {
    #region Public Methods and Operators

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