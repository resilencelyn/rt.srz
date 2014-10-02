// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageFaultInspector.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The message fault inspector.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.wcf
{
  #region references

  using System;
  using System.IO;
  using System.Runtime.Serialization;
  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.ServiceModel.Configuration;
  using System.ServiceModel.Description;
  using System.ServiceModel.Dispatcher;
  using System.Xml;

  #endregion

  /// <summary>
  /// The message fault inspector.
  /// </summary>
  public class MessageFaultInspector : BehaviorExtensionElement, IClientMessageInspector, IEndpointBehavior
  {
    #region Public Properties

    /// <summary>
    /// Gets the behavior type.
    /// </summary>
    public override Type BehaviorType
    {
      get
      {
        return typeof(MessageFaultInspector);
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The add binding parameters.
    /// </summary>
    /// <param name="endpoint">
    /// The endpoint.
    /// </param>
    /// <param name="parameters">
    /// The parameters.
    /// </param>
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection parameters)
    {
    }

    /// <summary>
    /// The after receive reply.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="correlationState">
    /// The correlation state.
    /// </param>
    /// <exception cref="Exception">
    /// </exception>
    public void AfterReceiveReply(ref Message message, object correlationState)
    {
      if (!message.IsFault)
      {
        return;
      }

      // Create a copy of the original reply to allow default WCF processing
      var buffer = message.CreateBufferedCopy(int.MaxValue);
      var copy = buffer.CreateMessage(); // Create a copy to work with
      message = buffer.CreateMessage(); // Restore the original message 

      var faultDetail = ReadFaultDetail(copy);
      var exception = faultDetail as Exception;
      if (exception != null)
      {
        throw exception;
      }
    }

    /// <summary>
    /// The apply client behavior.
    /// </summary>
    /// <param name="serviceEndpoint">
    /// The service endpoint.
    /// </param>
    /// <param name="behavior">
    /// The behavior.
    /// </param>
    public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
    {
      behavior.MessageInspectors.Add(new MessageFaultInspector());
    }

    /// <summary>
    /// The apply dispatch behavior.
    /// </summary>
    /// <param name="serviceEndpoint">
    /// The service endpoint.
    /// </param>
    /// <param name="dispatcher">
    /// The dispatcher.
    /// </param>
    public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher dispatcher)
    {
    }

    /// <summary>
    /// The before send request.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="channel">
    /// The channel.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public object BeforeSendRequest(ref Message message, IClientChannel channel)
    {
      return null;
    }

    /// <summary>
    /// The validate.
    /// </summary>
    /// <param name="serviceEndpoint">
    /// The service endpoint.
    /// </param>
    public void Validate(ServiceEndpoint serviceEndpoint)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    /// The create behavior.
    /// </summary>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    protected override object CreateBehavior()
    {
      return new MessageFaultInspector();
    }

    /// <summary>
    /// The read fault detail.
    /// </summary>
    /// <param name="reply">
    /// The reply.
    /// </param>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    private static object ReadFaultDetail(Message reply)
    {
      const string detailElementName = "Detail";

      using (var reader = reply.GetReaderAtBodyContents())
      {
        // Find <soap:Detail>
        while (reader.Read())
        {
          if (reader.NodeType == XmlNodeType.Element && reader.LocalName == detailElementName)
          {
            break;
          }
        }

        // Did we find it?
        if (reader.NodeType != XmlNodeType.Element || reader.LocalName != detailElementName)
        {
          return null;
        }

        // Move to the contents of <soap:Detail>
        if (!reader.Read())
        {
          return null;
        }

        // Deserialize the fault
        var serializer = new NetDataContractSerializer();
        try
        {
          return serializer.ReadObject(reader);
        }
          
          // TODO:
        catch (FileNotFoundException)
        {
          // Serializer was unable to find assembly where exception is defined 
          return null;
        }
      }
    }

    #endregion
  }
}