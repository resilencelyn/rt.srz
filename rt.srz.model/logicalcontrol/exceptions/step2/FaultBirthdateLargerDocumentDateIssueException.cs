// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultBirthdateLargerDocumentDateIssueException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birthdate larger document date issue exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault birthdate larger document date issue exception.
  /// </summary>
  [Serializable]
  public class FaultBirthdateLargerDocumentDateIssueException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultBirthdateLargerDocumentDateIssueException" /> class.
    /// </summary>
    public FaultBirthdateLargerDocumentDateIssueException()
      : base(
        new ExceptionInfo(Resource.FaultBirthdateLargerDocumentDateIssueCode), 
        Resource.FaultBirthdateLargerDocumentDateIssueMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateLargerDocumentDateIssueException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthdateLargerDocumentDateIssueException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}