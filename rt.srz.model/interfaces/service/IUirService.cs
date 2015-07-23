// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUirService.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The UIRGate interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service
{
  #region

  using System.ServiceModel;

  using rt.srz.model.interfaces.service.uir;

  #endregion

  /// <summary>
  ///   The UIRGate interface.
  /// </summary>
  [ServiceContract]
  public interface IUirService
  {
    #region Public Methods and Operators

    /// <summary>
    /// The get med ins state.
    /// </summary>
    /// <param name="request">
    /// The request.
    /// </param>
    /// <returns>
    /// The <see cref="Response"/> .
    /// </returns>
    [OperationContract(Action = "urn:#GetMedInsState", 
      ReplyAction = "http://new.webservice.namespace/IUIRGate/GetMedInsStateResponse")]
    [FaultContract(typeof(UIRResponse), Action = "urn:#GetMedInsState", Name = "UIRResponse", 
      Namespace = "http://uir.ffoms.ru")]
    [XmlSerializerFormat(SupportFaults = true)]
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
    [OperationContract(Action = "urn:#GetMedInsState2", 
      ReplyAction = "http://new.webservice.namespace/IUIRGate/GetMedInsState2Response")]
    [FaultContract(typeof(UIRResponse), Action = "urn:#GetMedInsState2", Name = "UIRResponse", 
      Namespace = "http://uir.ffoms.ru")]
    [XmlSerializerFormat(SupportFaults = true)]
    Response GetMedInsState2(Request2 request);

    #endregion
  }
}