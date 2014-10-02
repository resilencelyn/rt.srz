#region

using System.ServiceModel;
using System.ServiceModel.Activation;

using rt.core.services.aspects;
using rt.core.services.nhibernate;
using rt.core.services.wcf;

#endregion

namespace rt.srz.services.Uir
{
    [NHibernateWcfContext]
    [ErrorHandlingBehavior]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UirGate : UirGateInternal
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UirGate"/> class.
        /// </summary>
        public UirGate()
        {
            Interceptors.Add(new NHibernateProxyInterceptorServer());
        }

        #endregion
    }
}
