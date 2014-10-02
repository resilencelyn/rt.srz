// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.client
{
  #region

  using System;
  using System.Collections.Generic;

  using rt.core.services.aspects;
  using rt.core.services.registry;
  using rt.srz.model.dto;
  using rt.srz.model.HL7.person.messages;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.interfaces.service.uir;
  using rt.srz.model.srz;

  using Serialize.Linq.Nodes;

  #endregion

  /// <summary>
  ///   The atl client.
  /// </summary>
  public class UirClient : ServiceClient<IUirService>, IUirService
  {
    #region Constructors and Destructors

    /// <summary>
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
    /// <param name="className">
    /// тип валидатора 
    /// </param>
    public Response GetMedInsState(Request request)
    {
      return InvokeInterceptors(() => Service.GetMedInsState(request));
    }

    public Response GetMedInsState2(Request2 request)
    {
      return InvokeInterceptors(() => Service.GetMedInsState2(request));
    }

    #endregion
  }
}