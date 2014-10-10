// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPoliceCertificateFutureException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault police certificate future exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault police certificate future exception.
  /// </summary>
  [Serializable]
  public class FaultPoliceCertificateFutureException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateFutureException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateFutureException()
      : base(new ExceptionInfo("99"), Resource.FaultPoliceCertificateFutureExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateFutureException"/> class. 
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateFutureException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}