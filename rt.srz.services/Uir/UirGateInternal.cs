#region

using System;
using System.Collections.Generic;

using rt.core.services.aspects;
using rt.srz.model.enumerations;
using rt.srz.model.interfaces.service;
using rt.srz.model.interfaces.service.uir;
using rt.srz.model.srz;


#endregion

namespace rt.srz.services.Uir
{
    public class UirGateInternal :InterceptedBase, IUirService
    {
        #region Fields

        /// <summary>
        ///   The service.
        /// </summary>
        private readonly IUirService Service = new UirService();

        #endregion

        #region Public Methods and Operators
        public Response GetMedInsState(Request request)
        {
            return InvokeInterceptors(() => Service.GetMedInsState(request));
        }

        public Response GetMedInsState2(Request2 request)
        {
            return InvokeInterceptors(() => Service.GetMedInsState2(request));
        }
        #endregion
    }
}
