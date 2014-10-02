// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NHibernateWcfContextAttribute.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������� ��� ��������� ��������� ����������
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
  ///   ������� ��� ��������� ��������� ����������
  /// </summary>
  public sealed class NHibernateWcfContextAttribute : Attribute, IServiceBehavior
  {
    #region Public Methods and Operators

    /// <summary>
    /// ���������� ��������
    /// </summary>
    /// <param name="serviceDescription">
    /// ����������
    /// </param>
    /// <param name="serviceHostBase">
    /// ����
    /// </param>
    /// <param name="endpoints">
    /// ��������
    /// </param>
    /// <param name="bindingParameters">
    /// ���������
    /// </param>
    public void AddBindingParameters(
      ServiceDescription serviceDescription, 
      ServiceHostBase serviceHostBase, 
      Collection<ServiceEndpoint> endpoints, 
      BindingParameterCollection bindingParameters)
    {
    }

    /// <summary>
    /// ���������� ����������
    /// </summary>
    /// <param name="serviceDescription">
    /// ����������
    /// </param>
    /// <param name="serviceHostBase">
    /// ����
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
    /// ���������
    /// </summary>
    /// <param name="serviceDescription">
    /// ����������
    /// </param>
    /// <param name="serviceHostBase">
    /// ����
    /// </param>
    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
    }

    #endregion
  }
}