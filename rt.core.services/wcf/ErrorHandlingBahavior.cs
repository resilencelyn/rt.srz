// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHandlingBahavior.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The error handling behavior attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.wcf
{
  #region references

  using System;
  using System.Collections.ObjectModel;
  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.ServiceModel.Description;
  using System.ServiceModel.Dispatcher;

  #endregion

  /// <summary>
  /// The error handling behavior attribute.
  /// </summary>
  public sealed class ErrorHandlingBehaviorAttribute : Attribute, IServiceBehavior
  {
    #region Public Methods and Operators

    /// <summary>
    /// The add binding parameters.
    /// </summary>
    /// <param name="description">
    /// The description.
    /// </param>
    /// <param name="serviceHostBase">
    /// The service host base.
    /// </param>
    /// <param name="endpoints">
    /// The endpoints.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    public void AddBindingParameters(
      ServiceDescription description, 
      ServiceHostBase serviceHostBase, 
      Collection<ServiceEndpoint> endpoints, 
      BindingParameterCollection parameters)
    {
    }

    /// <summary>
    /// The apply dispatch behavior.
    /// </summary>
    /// <param name="serviceDescription">
    /// The service description.
    /// </param>
    /// <param name="serviceHostBase">
    /// The service host base.
    /// </param>
    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
      foreach (var chanDispBase in
        serviceHostBase.ChannelDispatchers)
      {
        var channelDispatcher = chanDispBase as ChannelDispatcher;
        if (channelDispatcher == null)
        {
          continue;
        }

        channelDispatcher.ErrorHandlers.Add(new ExceptionErrorHandler());
      }
    }

    /// <summary>
    /// The validate.
    /// </summary>
    /// <param name="description">
    /// The description.
    /// </param>
    /// <param name="serviceHostBase">
    /// The service host base.
    /// </param>
    public void Validate(ServiceDescription description, ServiceHostBase serviceHostBase)
    {
    }

    #endregion
  }
}