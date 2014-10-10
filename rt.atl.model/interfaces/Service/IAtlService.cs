// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAtlService.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The AtlGate interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.interfaces.Service
{
  using System.ServiceModel;

  using Quartz;

  /// <summary>
  ///   The AtlGate interface.
  /// </summary>
  [ServiceContract]
  public interface IAtlService
  {
    #region Public Methods and Operators

    /// <summary>
    /// The run export to pvp.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunExportToPvp(IJobExecutionContext context);

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunExportToSrz(IJobExecutionContext context);

    /// <summary>
    /// The run export to srz.
    /// </summary>
    /// <param name="context">
    /// The context.
    /// </param>
    [OperationContract]
    void RunSinhronizeNsi(IJobExecutionContext context);

    #endregion
  }
}