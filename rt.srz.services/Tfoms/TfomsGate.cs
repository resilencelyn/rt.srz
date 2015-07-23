// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TfomsGate.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The tf gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Tfoms
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  ///   The tf gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class TfomsGate : TfomsGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="TfomsGate" /> class.
    /// </summary>
    public TfomsGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}