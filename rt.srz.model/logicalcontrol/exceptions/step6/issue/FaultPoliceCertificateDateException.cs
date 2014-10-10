// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPoliceCertificateDateException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault police certificate date exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault police certificate date exception.
  /// </summary>
  [Serializable]
  public class FaultPoliceCertificateDateException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateException()
      : base(
        new ExceptionInfo(Resource.FaultPoliceCertificateDateExceptionCode), 
        Resource.FaultPoliceCertificateDateExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateException"/> class. 
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateDateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}