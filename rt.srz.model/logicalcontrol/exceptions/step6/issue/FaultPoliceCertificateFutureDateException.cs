// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPoliceCertificateFutureDateException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault police certificate future date exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault police certificate future date exception.
  /// </summary>
  [Serializable]
  public class FaultPoliceCertificateFutureDateException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateFutureDateException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateFutureDateException()
      : base(
        new ExceptionInfo(Resource.FaultPoliceCertificateFutureDateExceptionCode), 
        Resource.FaultPoliceCertificateFutureDateExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateFutureDateException"/> class. 
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateFutureDateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}