using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using rt.srz.model.interfaces.service.uir;

namespace rt.srz.business.manager
{
    /// <summary>
    ///   The interface UirManager.
    /// </summary>
    public interface IUirManager
    {
        /// <summary>
        /// The get med ins state.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// The <see cref="Response"/> . 
        /// </returns>
        Response GetMedInsState(Request request);

        /// <summary>
        /// The get med ins state 2.
        /// </summary>
        /// <param name="request">
        /// The request. 
        /// </param>
        /// <returns>
        /// The <see cref="Response"/> . 
        /// </returns>
        Response GetMedInsState2(Request2 request);
    }
}
