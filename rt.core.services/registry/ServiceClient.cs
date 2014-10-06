// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceClient.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The service client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.services.registry
{
  #region references

  using System.ServiceModel;

  using rt.core.services.aspects;

  #endregion

  /// <summary>
  /// The service client.
  /// </summary>
  /// <typeparam name="TInterface">
  /// TInterface
  /// </typeparam>
  public abstract class ServiceClient<TInterface> : InterceptedBase
    where TInterface : class
  {
    #region Fields

    /// <summary>
    ///   Прокси
    /// </summary>
    private WcfProxy<TInterface> proxy;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ServiceClient{TInterface}" /> class.
    /// </summary>
    protected ServiceClient()
    {
      Interceptors.Add(new NHibernateProxyInterceptorClient());
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Сервис
    /// </summary>
    protected TInterface Service
    {
      get
      {
        if (proxy == null || proxy.State != CommunicationState.Opened)
        {
          if (proxy != null)
          {
            proxy.Dispose();
          }

          proxy = new WcfProxy<TInterface>();
          proxy.Open();
        }

        return proxy.Service;
      }
    }

    #endregion
  }
}