// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NsiGate.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nsi gate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.NSI
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
  public class NsiGate : NsiGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="NsiGate" /> class.
    /// </summary>
    public NsiGate()
    {
      Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}