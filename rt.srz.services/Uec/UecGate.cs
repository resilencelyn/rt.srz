// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UecGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The uec gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Uec
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;
  using rt.uec.model.Interfaces;

  #endregion

  /// <summary>
  /// The uec gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class UecGate : UecGateInternal, IUecService
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="UecGate"/> class.
    /// </summary>
    public UecGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}