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
  using Quartz;

  using rt.atl.model.interfaces.service;
  using rt.core.services.aspects;

  /// <summary>
  ///   The atl service.
  /// </summary>
  public class AtlGateInternal : InterceptedBase, IAtlService
  {
    #region Fields

    /// <summary>
    ///   The services.
    /// </summary>
    private readonly IAtlService services = new AtlService();

    #endregion

    #region Public Methods and Operators

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