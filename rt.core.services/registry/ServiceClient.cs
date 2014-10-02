// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceClient.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
        /// <summary>
        ///   Прокси
        /// </summary>
        private WcfProxy<TInterface> proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClient{TInterface}"/> class.
        /// </summary>
        protected ServiceClient()
        {
            Interceptors.Add(new NHibernateProxyInterceptorClient());
        }

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
    }
}