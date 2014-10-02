// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SmoGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The smo gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Smo
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  /// The smo gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class SmoGate : SmoGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SmoGate"/> class.
    /// </summary>
    public SmoGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}