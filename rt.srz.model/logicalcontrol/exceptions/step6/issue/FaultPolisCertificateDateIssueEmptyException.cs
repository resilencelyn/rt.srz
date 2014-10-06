// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPolisCertificateDateIssueEmptyException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultPolisCertificateDateIssueEmptyException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPolisCertificateDateIssueEmptyException" /> class.
    /// </summary>
    public FaultPolisCertificateDateIssueEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultPolisCertificateDateIssueEmptyExceptionCode), 
        Resource.FaultPolisCertificateDateIssueEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPolisCertificateDateIssueEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPolisCertificateDateIssueEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}