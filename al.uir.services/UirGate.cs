using System.ServiceModel;
using System.ServiceModel.Activation;
using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.wcf;

namespace al.uir.services
{
  /// <summary>
  ///   The uir gate.
  /// </summary>
  [ErrorHandlingBehavior]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class UirGate : UirGateInternal
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="UirGate" /> class.
    /// </summary>
    public UirGate()
    {
      //Interceptors.Add(new NHibernateProxyInterceptorServer());
    }

    #endregion
  }
}