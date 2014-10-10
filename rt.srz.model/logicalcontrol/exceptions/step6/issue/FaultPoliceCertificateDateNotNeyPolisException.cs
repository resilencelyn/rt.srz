// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPoliceCertificateDateNotNeyPolisException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault police certificate date not ney polis exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault police certificate date not ney polis exception.
  /// </summary>
  [Serializable]
  public class FaultPoliceCertificateDateNotNeyPolisException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateNotNeyPolisException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    public FaultPoliceCertificateDateNotNeyPolisException()
      : base(
        new ExceptionInfo(Resource.FaultPoliceCertificateDateNotNeyPolisExceptionCode), 
        Resource.FaultPoliceCertificateDateNotNeyPolisExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPoliceCertificateDateNotNeyPolisException"/> class. 
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPoliceCertificateDateNotNeyPolisException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}