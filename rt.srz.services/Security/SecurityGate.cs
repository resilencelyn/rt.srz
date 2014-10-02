// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The security gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Security
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  /// The security gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class SecurityGate : SecurityGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityGate"/> class.
    /// </summary>
    public SecurityGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}