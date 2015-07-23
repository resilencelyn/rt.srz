// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirGateInternal.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir gate internal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Uir
{
  using rt.core.services.aspects;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.interfaces.service.uir;

  /// <summary>
  ///   The uir gate internal.
  /// </summary>
  public class UirGateInternal : InterceptedBase, IUirService
  {
    #region Fields

    /// <summary>
    ///   The service.
    /// </summary>
    protected readonly IUirService Service = new UirService();

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The get med ins state.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/>.
    /// </returns>
    public Response GetMedInsState(Request request)
    {
      return InvokeInterceptors(() => Service.GetMedInsState(request));
    }

    /// <summary>
    /// The get med ins state 2.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/>.
    /// </returns>
    public Response GetMedInsState2(Request2 request)
    {
      return InvokeInterceptors(() => Service.GetMedInsState2(request));
    }

    #endregion
  }
}