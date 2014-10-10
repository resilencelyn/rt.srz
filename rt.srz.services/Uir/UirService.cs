// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UirService.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The uir service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.services.Uir
{
  using System.ServiceModel;

  using rt.core.services.nhibernate;
  using rt.core.services.wcf;
  using rt.srz.business.manager;
  using rt.srz.model.interfaces.service;
  using rt.srz.model.interfaces.service.uir;
  using rt.srz.model.srz;

  using StructureMap;

  /// <summary>
  ///   The uir service.
  /// </summary>
  [NHibernateWcfContext]
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
  [ErrorHandlingBehavior]
  public class UirService : IUirService
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
      return ObjectFactory.GetInstance<IUirManager>().GetMedInsState(request);
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
      return ObjectFactory.GetInstance<IUirManager>().GetMedInsState2(request);
    }

    #endregion
  }
}