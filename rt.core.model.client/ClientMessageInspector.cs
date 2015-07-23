// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientMessageInspector.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Добавляет кридентиалы для
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.client
{
  #region references

  using System;
  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.ServiceModel.Configuration;
  using System.ServiceModel.Description;
  using System.ServiceModel.Dispatcher;

  #endregion

  /// <summary>
  ///   Добавляет кридентиалы для
  /// </summary>
  public class ClientMessageInspector : BehaviorExtensionElement, IClientMessageInspector, IEndpointBehavior
  {
    #region Static Fields

    /// <summary>
    ///   Пароли и явки
    /// </summary>
    public static readonly Credentials Сredentials = new Credentials();

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets BehaviorType.
    /// </summary>
    public override Type BehaviorType
    {
      get
      {
        return GetType();
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// AddBindingParameters
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint.
    /// </param>
    /// <param name="bindingParameters">
    /// The binding parameters.
    /// </param>
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// После ответа
    /// </summary>
    /// <param name="reply">
    /// The reply.
    /// </param>
    /// <param name="correlationState">
    /// The correlation state.
    /// </param>
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
    }

    /// <summary>
    /// ApplyClientBehavior
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint.
    /// </param>
    /// <param name="clientRuntime">
    /// The client runtime.
    /// </param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
      clientRuntime.MessageInspectors.Add(this);
    }

    /// <summary>
    /// ApplyDispatchBehavior
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint.
    /// </param>
    /// <param name="endpointDispatcher">
    /// The endpoint dispatcher.
    /// </param>
    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
    }

    /// <summary>
    /// Перед запросом
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <param name="channel">
    /// The channel.
    /// </param>
    /// <returns>
    /// null
    /// </returns>
    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
      request.Headers.Add(Сredentials.ToMessageHeader());
      return null;
    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint.
    /// </param>
    public void Validate(ServiceEndpoint endpoint)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    ///   CreateBehavior
    /// </summary>
    /// <returns>
    ///   this
    /// </returns>
    protected override object CreateBehavior()
    {
      return this;
    }

    #endregion
  }
}