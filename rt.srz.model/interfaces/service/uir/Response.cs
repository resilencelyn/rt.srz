// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Response.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.interfaces.service.uir
{
  #region

  using System.CodeDom.Compiler;
  using System.Diagnostics;
  using System.ServiceModel;

  #endregion

  /// <summary>
  ///   The response.
  /// </summary>
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class Response
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Response" /> class.
    /// </summary>
    public Response()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Response"/> class.
    /// </summary>
    /// <param name="uirResponse">
    /// The uir response.
    /// </param>
    public Response(UIRResponse uirResponse)
    {
      this.UIRResponse = uirResponse;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   The uir response.
    /// </summary>
    [MessageBodyMember(Namespace = "http://uir.ffoms.ru", Order = 0)]
    public UIRResponse UIRResponse { get; set; }

    #endregion
  }
}