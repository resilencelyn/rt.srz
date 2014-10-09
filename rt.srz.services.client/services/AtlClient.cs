// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtlClient.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region references

  using Quartz;

  using rt.atl.model.interfaces.service;
  using rt.core.services.registry;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class AtlClient : ServiceClient<IAtlService>, IAtlService
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