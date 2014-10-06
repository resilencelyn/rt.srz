// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TokenMessageInspector.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Инспектор для сообщений
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
  ///   Инспектор для сообщений
  /// </summary>
  public class TokenMessageInspector : BehaviorExtensionElement, IClientMessageInspector, IEndpointBehavior
  {
    #region Public Properties

    /// <summary>
    ///   Функция для задания токенов
    /// </summary>
    public static Func<Token> TokenFunc { get; set; }

    /// <summary>
    ///   Gets the type of behavior.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Type" />.
    /// </returns>
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
    /// Implement to pass data at runtime to bindings to support custom behavior.
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint to modify.
    /// </param>
    /// <param name="bindingParameters">
    /// The objects that binding elements require to support the behavior.
    /// </param>
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the
    ///   client application.
    /// </summary>
    /// <param name="reply">
    /// The message to be transformed into types and handed back to the client application.
    /// </param>
    /// <param name="correlationState">
    /// Correlation state data.
    /// </param>
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
    }

    /// <summary>
    /// Implements a modification or extension of the client across an endpoint.
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint that is to be customized.
    /// </param>
    /// <param name="clientRuntime">
    /// The client runtime to be customized.
    /// </param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
      clientRuntime.MessageInspectors.Add(this);
    }

    /// <summary>
    /// Implements a modification or extension of the service across an endpoint.
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint that exposes the contract.
    /// </param>
    /// <param name="endpointDispatcher">
    /// The endpoint dispatcher to be modified or extended.
    /// </param>
    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
    }

    /// <summary>
    /// Enables inspection or modification of a message before a request message is sent to a service.
    /// </summary>
    /// <returns>
    /// The object that is returned as the <paramref name="correlationState "/>argument of the
    ///   <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)"/>
    ///   method. This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid"/>
    ///   to ensure that no two <paramref name="correlationState"/> objects are the same.
    /// </returns>
    /// <param name="request">
    /// The message to be sent to the service.
    /// </param>
    /// <param name="channel">
    /// The  client object channel.
    /// </param>
    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
      try
      {
        Token token = null;
        if (TokenFunc != null)
        {
          token = TokenFunc();
        }

        // else
        // {
        // 	dynamic identity = Thread.CurrentPrincipal.Identity;
        // 	token = identity.Ticket.UserData;
        // }
        request.Headers.Add(new TokenCredentials(token).ToMessageHeader());
      }
      catch
      {
      }

      return null;
    }

    /// <summary>
    /// Implement to confirm that the endpoint meets some intended criteria.
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint to validate.
    /// </param>
    public void Validate(ServiceEndpoint endpoint)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    ///   Creates a behavior extension based on the current configuration settings.
    /// </summary>
    /// <returns>
    ///   The behavior extension.
    /// </returns>
    protected override object CreateBehavior()
    {
      return this;
    }

    #endregion
  }
}