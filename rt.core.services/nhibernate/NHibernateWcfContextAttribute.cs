// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateWcfContextAttribute.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Атрибут для включения контекста хибернейта
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.nhibernate
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
  ///   Атрибут для включения контекста хибернейта
  /// </summary>
  public sealed class NHibernateWcfContextAttribute : Attribute, IServiceBehavior
  {
    #region Public Methods and Operators

    /// <summary>
    /// Добавление биндинга
    /// </summary>
    /// <param name="serviceDescription">
    /// Дескрипшин
    /// </param>
    /// <param name="serviceHostBase">
    /// Хост
    /// </param>
    /// <param name="endpoints">
    /// Ендпоинт
    /// </param>
    /// <param name="bindingParameters">
    /// Параметры
    /// </param>
    public void AddBindingParameters(
      ServiceDescription serviceDescription, 
      ServiceHostBase serviceHostBase, 
      Collection<ServiceEndpoint> endpoints, 
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// Применение диспатчера
    /// </summary>
    /// <param name="serviceDescription">
    /// Дескрипшин
    /// </param>
    /// <param name="serviceHostBase">
    /// Хост
    /// </param>
    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
      foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
      {
        foreach (var endpoint in channelDispatcher.Endpoints)
        {
          endpoint.DispatchRuntime.MessageInspectors.Add(new NHibernateWcfContextInitializer());
        }
      }
    }

    /// <summary>
    /// Валидация
    /// </summary>
    /// <param name="serviceDescription">
    /// Дескрипшин
    /// </param>
    /// <param name="serviceHostBase">
    /// Хост
    /// </param>
    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
    }

    #endregion
  }
}