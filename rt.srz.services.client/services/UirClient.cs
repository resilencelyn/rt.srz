// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client.services
{
  #region

  using rt.core.services.aspects;
  using rt.core.services.registry;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.interfaces.service.uir;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class UirClient : ServiceClient<IUirService>, IUirService
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="UirClient" /> class.
    ///   Initializes a new instance of the <see cref="StatementClient" /> class.
    /// </summary>
    public UirClient()
    {
      Interceptors.Add(new NHibernateProxyInterceptorClient());
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// Добавляет в базу настройку о том что можно включать отключать проверку данного валидатора
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