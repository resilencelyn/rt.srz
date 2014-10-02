// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfProxy.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ������ ��� �������
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using rt.uec.model.Interfaces;

namespace rt.uec.service.serviceClient
{
  #region references

  

  #endregion

  /// <summary>
  /// ������ ��� �������
  /// </summary>
  /// <typeparam name="T">
  /// ��� �������
  /// </typeparam>
  public sealed class WcfProxy : ClientBase<IUecService>, IDisposable
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="WcfProxy"/> class.
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
    /// Initializes a new instance of the <see cref="WcfProxy"/> class.
    /// </summary>
    public WcfProxy()
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   ���������� ������
    /// </summary>
    public IUecService Service
    {
      get
      {
        return Channel;
      }
    }

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///   ������ �� � � ������ �����
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