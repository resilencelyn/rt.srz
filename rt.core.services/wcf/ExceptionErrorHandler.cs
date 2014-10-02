// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionErrorHandler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ExceptionMarshallingErrorHandler implements IErrorHandler to serialize unhandled
//   exceptions thrown by service code as fault messages. When an exception bubbles up
//   to WCF runtime, it calls ProvideFault method of the error handler to process the
//   exception. As you can see below, code in ProvideFault method uses NetDataContractSerializer
//   to serialize the exception and generate a fault message that will be sent to the client.
//   WCF then calls HandleError method to determine if it needs to perform standard error
//   handling for this exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.wcf
{
  #region references

  using System;
  using System.Runtime.Serialization;
  using System.ServiceModel;
  using System.ServiceModel.Channels;
  using System.ServiceModel.Dispatcher;

  #endregion

  /// <summary>
  ///   ExceptionMarshallingErrorHandler implements IErrorHandler to serialize unhandled
  ///   exceptions thrown by service code as fault messages. When an exception bubbles up
  ///   to WCF runtime, it calls ProvideFault method of the error handler to process the
  ///   exception. As you can see below, code in ProvideFault method uses NetDataContractSerializer
  ///   to serialize the exception and generate a fault message that will be sent to the client.
  ///   WCF then calls HandleError method to determine if it needs to perform standard error
  ///   handling for this exception.
  /// </summary>
  public class ExceptionErrorHandler : IErrorHandler
  {
    #region Explicit Interface Methods

    /// <summary>
    /// The handle error.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    bool IErrorHandler.HandleError(Exception error)
    {
      if (error is FaultException)
      {
        return false; // Let WCF do normal processing
      }

      return true; // Fault message is already generated
    }

    /// <summary>
    /// The provide fault.
    /// </summary>
    /// <param name="error">
    /// The error.
    /// </param>
    /// <param name="version">
    /// The version.
    /// </param>
    /// <param name="fault">
    /// The fault.
    /// </param>
    void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
    {
      if (error is FaultException)
      {
        // Let WCF do normal processing
      }
      else
      {
        // Generate fault message manually
        var messageFault = MessageFault.CreateFault(
          new FaultCode("Sender"), 
          new FaultReason(error.Message), 
          error, 
          new NetDataContractSerializer());
        fault = Message.CreateMessage(version, messageFault, null);
      }
    }

    #endregion
  }
}