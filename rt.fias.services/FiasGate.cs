// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FiasGate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.fias.services
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.model;
  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  ///   The kladr gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class FiasGate : FiasGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FiasGate" /> class.
    /// </summary>
    public FiasGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}