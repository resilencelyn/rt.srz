// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateDateIssueException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault temporary certificate date issue exception.
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
  ///   The fault temporary certificate date issue exception.
  /// </summary>
  [Serializable]
  public class FaultTemporaryCertificateDateIssueException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateDateIssueException" /> class.
    /// </summary>
    public FaultTemporaryCertificateDateIssueException()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateDateIssueExceptionCode), 
        Resource.FaultTemporaryCertificateDateIssueExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateDateIssueException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateDateIssueException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}