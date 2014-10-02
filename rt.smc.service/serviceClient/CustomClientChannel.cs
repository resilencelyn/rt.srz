// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomClientChannel.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Custom client channel. Allows to specify a different configuration file
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace rt.smc.service.serviceClient
{
  #region references

  

  #endregion

  /// <summary>
  /// Custom client channel. Allows to specify a different configuration file
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public class CustomClientChannel<T> : ChannelFactory<T>
  {
    #region Fields

    /// <summary>
    /// The configuration path.
    /// </summary>
    private readonly string configurationPath;

    /// <summary>
    /// The endpoint configuration name.
    /// </summary>
    private readonly string endpointConfigurationName;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(string configurationPath)
      : base(typeof(T))
    {
      this.configurationPath = configurationPath;
      this.InitializeEndpoint((string)null, null);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="binding">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(Binding binding, string configurationPath)
      : this(binding, (EndpointAddress)null, configurationPath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="serviceEndpoint">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(ServiceEndpoint serviceEndpoint, string configurationPath)
      : base(typeof(T))
    {
      this.configurationPath = configurationPath;
      this.InitializeEndpoint(serviceEndpoint);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="endpointConfigurationName">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(string endpointConfigurationName, string configurationPath)
      : this(endpointConfigurationName, null, configurationPath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="binding">
    /// </param>
    /// <param name="endpointAddress">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(Binding binding, EndpointAddress endpointAddress, string configurationPath)
      : base(typeof(T))
    {
      this.configurationPath = configurationPath;
      this.InitializeEndpoint(binding, endpointAddress);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="binding">
    /// </param>
    /// <param name="remoteAddress">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(Binding binding, string remoteAddress, string configurationPath)
      : this(binding, new EndpointAddress(remoteAddress), configurationPath)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomClientChannel{T}"/> class. 
    /// Constructor
    /// </summary>
    /// <param name="endpointConfigurationName">
    /// </param>
    /// <param name="endpointAddress">
    /// </param>
    /// <param name="configurationPath">
    /// </param>
    public CustomClientChannel(
      string endpointConfigurationName, 
      EndpointAddress endpointAddress, 
      string configurationPath)
      : base(typeof(T))
    {
      this.configurationPath = configurationPath;
      this.endpointConfigurationName = endpointConfigurationName;
      this.InitializeEndpoint(endpointConfigurationName, endpointAddress);
    }

    #endregion

    #region Methods

    /// <summary>
    /// The apply configuration.
    /// </summary>
    /// <param name="configurationName">
    /// The configuration name.
    /// </param>
    protected override void ApplyConfiguration(string configurationName)
    {
      // base.ApplyConfiguration(configurationName);
    }

    /// <summary>
    /// Loads the serviceEndpoint description from the specified configuration file
    /// </summary>
    /// <returns>
    /// The <see cref="ServiceEndpoint"/>.
    /// </returns>
    protected override ServiceEndpoint CreateDescription()
    {
      var serviceEndpoint = base.CreateDescription();

      if (endpointConfigurationName != null)
      {
        serviceEndpoint.Name = endpointConfigurationName;
      }

      var map = new ExeConfigurationFileMap();
      map.ExeConfigFilename = configurationPath;

      var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
      var group = ServiceModelSectionGroup.GetSectionGroup(config);

      ChannelEndpointElement selectedEndpoint = null;

      foreach (ChannelEndpointElement endpoint in group.Client.Endpoints)
      {
        if (endpoint.Contract == serviceEndpoint.Contract.ConfigurationName
            && (endpointConfigurationName == null || endpointConfigurationName == endpoint.Name))
        {
          selectedEndpoint = endpoint;
          break;
        }
      }

      if (selectedEndpoint != null)
      {
        if (serviceEndpoint.Binding == null)
        {
          serviceEndpoint.Binding = CreateBinding(selectedEndpoint.Binding, group);
        }

        if (serviceEndpoint.Address == null)
        {
          serviceEndpoint.Address = new EndpointAddress(
            selectedEndpoint.Address, 
            GetIdentity(selectedEndpoint.Identity), 
            selectedEndpoint.Headers.Headers);
        }

        if (serviceEndpoint.Behaviors.Count == 0 && selectedEndpoint.BehaviorConfiguration != null)
        {
          AddBehaviors(selectedEndpoint.BehaviorConfiguration, serviceEndpoint, group);
        }

        serviceEndpoint.Name = selectedEndpoint.Contract;
      }

      return serviceEndpoint;
    }

    /// <summary>
    /// Adds the configured behavior to the selected endpoint
    /// </summary>
    /// <param name="behaviorConfiguration">
    /// </param>
    /// <param name="serviceEndpoint">
    /// </param>
    /// <param name="group">
    /// </param>
    private void AddBehaviors(
      string behaviorConfiguration, 
      ServiceEndpoint serviceEndpoint, 
      ServiceModelSectionGroup group)
    {
      if (group.Behaviors.EndpointBehaviors.Count == 0)
      {
        return;
      }

      var behaviorElement = group.Behaviors.EndpointBehaviors[behaviorConfiguration];
      for (var i = 0; i < behaviorElement.Count; i++)
      {
        var behaviorExtension = behaviorElement[i];
        var extension = behaviorExtension.GetType()
          .InvokeMember(
            "CreateBehavior", 
            BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, 
            null, 
            behaviorExtension, 
            null);
        if (extension != null)
        {
          serviceEndpoint.Behaviors.Add((IEndpointBehavior)extension);
        }
      }
    }

    /// <summary>
    /// Configures the binding for the selected endpoint
    /// </summary>
    /// <param name="bindingName">
    /// </param>
    /// <param name="group">
    /// </param>
    /// <returns>
    /// The <see cref="Binding"/>.
    /// </returns>
    private Binding CreateBinding(string bindingName, ServiceModelSectionGroup group)
    {
      var bindingElementCollection = group.Bindings[bindingName];
      if (bindingElementCollection.ConfiguredBindings.Count > 0)
      {
        var be = bindingElementCollection.ConfiguredBindings[0];

        var binding = GetBinding(be);
        if (be != null)
        {
          be.ApplyConfiguration(binding);
        }

        return binding;
      }

      return null;
    }

    /// <summary>
    /// Helper method to create the right binding depending on the configuration element
    /// </summary>
    /// <param name="configurationElement">
    /// </param>
    /// <returns>
    /// The <see cref="Binding"/>.
    /// </returns>
    private Binding GetBinding(IBindingConfigurationElement configurationElement)
    {
      if (configurationElement is CustomBindingElement)
      {
        return new CustomBinding();
      }

      if (configurationElement is BasicHttpBindingElement)
      {
        return new BasicHttpBinding();
      }

      if (configurationElement is NetMsmqBindingElement)
      {
        return new NetMsmqBinding();
      }

      if (configurationElement is NetNamedPipeBindingElement)
      {
        return new NetNamedPipeBinding();
      }

      if (configurationElement is NetPeerTcpBindingElement)
      {
        return new NetPeerTcpBinding();
      }

      if (configurationElement is NetTcpBindingElement)
      {
        return new NetTcpBinding();
      }

      if (configurationElement is WSDualHttpBindingElement)
      {
        return new WSDualHttpBinding();
      }

      if (configurationElement is WSHttpBindingElement)
      {
        return new WSHttpBinding();
      }

      if (configurationElement is WSFederationHttpBindingElement)
      {
        return new WSFederationHttpBinding();
      }

      return null;
    }

    /// <summary>
    /// Gets the endpoint identity from the configuration file
    /// </summary>
    /// <param name="element">
    /// </param>
    /// <returns>
    /// The <see cref="EndpointIdentity"/>.
    /// </returns>
    private EndpointIdentity GetIdentity(IdentityElement element)
    {
      EndpointIdentity identity = null;
      var properties = element.ElementInformation.Properties;
      if (properties["userPrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
      {
        return EndpointIdentity.CreateUpnIdentity(element.UserPrincipalName.Value);
      }

      if (properties["servicePrincipalName"].ValueOrigin != PropertyValueOrigin.Default)
      {
        return EndpointIdentity.CreateSpnIdentity(element.ServicePrincipalName.Value);
      }

      if (properties["dns"].ValueOrigin != PropertyValueOrigin.Default)
      {
        return EndpointIdentity.CreateDnsIdentity(element.Dns.Value);
      }

      if (properties["rsa"].ValueOrigin != PropertyValueOrigin.Default)
      {
        return EndpointIdentity.CreateRsaIdentity(element.Rsa.Value);
      }

      if (properties["certificate"].ValueOrigin != PropertyValueOrigin.Default)
      {
        var supportingCertificates = new X509Certificate2Collection();
        supportingCertificates.Import(Convert.FromBase64String(element.Certificate.EncodedValue));
        if (supportingCertificates.Count == 0)
        {
          throw new InvalidOperationException("UnableToLoadCertificateIdentity");
        }

        var primaryCertificate = supportingCertificates[0];
        supportingCertificates.RemoveAt(0);
        return EndpointIdentity.CreateX509CertificateIdentity(primaryCertificate, supportingCertificates);
      }

      return identity;
    }

    #endregion
  }
}