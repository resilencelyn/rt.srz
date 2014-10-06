// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The request.
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
  ///   The request.
  /// </summary>
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class Request
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Request" /> class.
    /// </summary>
    public Request()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Request"/> class.
    /// </summary>
    /// <param name="uirRequest">
    /// The uir request.
    /// </param>
    public Request(UIRRequest uirRequest)
    {
      UIRRequest = uirRequest;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   The uir request.
    /// </summary>
    [MessageBodyMember(Namespace = "http://uir.ffoms.ru", Order = 0)]
    public UIRRequest UIRRequest { get; set; }

    #endregion
  }
}