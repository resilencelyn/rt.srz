// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Request2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The request 2.
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
  ///   The request 2.
  /// </summary>
  [DebuggerStepThrough]
  [GeneratedCode("System.ServiceModel", "3.0.0.0")]
  [MessageContract(IsWrapped = false)]
  public class Request2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Request2" /> class.
    /// </summary>
    public Request2()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Request2"/> class.
    /// </summary>
    /// <param name="UIRRequest2">
    /// The uir request 2. 
    /// </param>
    public Request2(UIRRequest2 UIRRequest2)
    {
      this.UIRRequest2 = UIRRequest2;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   The uir request 2.
    /// </summary>
    [MessageBodyMember(Namespace = "http://uir.ffoms.ru", Order = 0)]
    public UIRRequest2 UIRRequest2 { get; set; }

    #endregion
  }
}