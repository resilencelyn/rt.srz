// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using StructureMap;
using al.uir.services.UIRGateContract;

namespace rt.srz.services.Uir
{
  using System.ServiceModel;

  using rt.core.services.nhibernate;
  using rt.core.services.wcf;

  /// <summary>
  ///   The uir service.
  /// </summary>
  [NHibernateWcfContext]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [ErrorHandlingBehavior]
  public class UirService : IUIRGate
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get med ins state.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Response GetMedInsState(Request request)
    {
      return ObjectFactory.GetInstance<IUIRGate>().GetMedInsState(request);
    }

    /// <summary>
    /// The get med ins state 2.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Kladr"/> .
    /// </returns>
    public Response GetMedInsState2(Request2 request)
    {
      return ObjectFactory.GetInstance<IUIRGate>().GetMedInsState2(request);
    }

    #endregion
  }
}