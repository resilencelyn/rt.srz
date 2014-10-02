// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfProxy.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Прокси для сервиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace rt.smc.service.serviceClient
{
  #region references

  

  #endregion

  /// <summary>
  /// Прокси для сервиса
  /// </summary>
  /// <typeparam name="T">
  /// Тип сервиса
  /// </typeparam>
  public sealed class WcfProxy<T> : ClientBase<T>, IDisposable
    where T : class
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WcfProxy{T}"/> class.
    /// </summary>
    /// <param name="binding">
    /// The binding.
    /// </param>
    /// <param name="remoteAddress">
    /// The remote address.
    /// </param>
    public WcfProxy(Binding binding, EndpointAddress remoteAddress)
      : base(binding, remoteAddress)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WcfProxy{T}"/> class.
    /// </summary>
    public WcfProxy()
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Возвращает сервис
    /// </summary>
    public T Service
    {
      get
      {
        return Channel;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   Диспоз он и в африке такой
    /// </summary>
    public void Dispose()
    {
      switch (State)
      {
        case CommunicationState.Closed:
          break; // nothing to do
        case CommunicationState.Faulted:
          Abort();
          break;
        case CommunicationState.Opened:
          Close();
          break;
      }
    }

    #endregion
  }
}