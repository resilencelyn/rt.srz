// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEmptySignatureException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault empty signature exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step5
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault empty signature exception.
  /// </summary>
  [Serializable]
  public class FaultEmptySignatureException : FaultStep5
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEmptySignatureException" /> class.
    /// </summary>
    public FaultEmptySignatureException()
      : base(new ExceptionInfo(Resource.FaultEmptySignatureExceptionCode), Resource.FaultEmptySignatureExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmptySignatureException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEmptySignatureException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}