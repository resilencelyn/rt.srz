// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TFGate.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The tf gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.TF
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  /// The tf gate.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class TFGate : TFGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="TFGate"/> class.
    /// </summary>
    public TFGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}