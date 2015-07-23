// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegulatoryGate.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Regulatory
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  ///   The nsi gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class RegulatoryGate : RegulatoryGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="RegulatoryGate" /> class.
    /// </summary>
    public RegulatoryGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}