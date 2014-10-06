// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WcfProxy.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Прокси для сервиса
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.registry
{
  #region references

  using System;
  using System.ServiceModel;

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