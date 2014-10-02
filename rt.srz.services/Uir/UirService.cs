#region

using System;
using System.Collections.Generic;
using System.ServiceModel;

using rt.core.services.nhibernate;
using rt.core.services.wcf;
using rt.srz.business.manager;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.srz;
using rt.srz.model.interfaces.service.uir;

using StructureMap;

#endregion

namespace rt.srz.services.Uir
{
    
    [NHibernateWcfContext]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [ErrorHandlingBehavior]
    public class UirService : IUirService
    {

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tfom">
        /// The tfom. 
        /// </param>
        /// <returns>
        /// The <see cref="Kladr"/> . 
        /// </returns>
        public Response GetMedInsState(Request request)
        {
            return ObjectFactory.GetInstance<IUirManager>().GetMedInsState(request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectId">
        /// </param>
        /// <returns>
        /// The <see cref="Kladr"/> . 
        /// </returns>
        public Response GetMedInsState2(Request2 request)
        {
            return ObjectFactory.GetInstance<IUirManager>().GetMedInsState2(request);
        }

        #endregion
    }
}
