// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatementGate.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statement gate impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Statement
{
  #region

  using System.ServiceModel;
  using System.ServiceModel.Activation;

  using rt.core.services.aspects;
  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  #endregion

  /// <summary>
  ///   The statement gate impl.
  /// </summary>
  [NHibernateWcfContext]
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class StatementGate : StatementGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatementGate" /> class.
    /// </summary>
    public StatementGate()
    {
      Interceptors.Clear();
      Interceptors.Add(new LoggingInterceptorStatement());
      Interceptors.Add(new NHibernateProxyInterceptorServer(3));
    }

    #endregion
  }
}