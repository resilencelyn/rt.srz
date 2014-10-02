// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.ServiceModel;
using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.wcf;

#endregion

namespace rt.srz.services.Statement
{
  /// <summary>
  ///   The statement gate impl.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  public class StatementGate : StatementGateInternal
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="StatementGate"/> class.
    /// </summary>
    public StatementGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }
  }
}