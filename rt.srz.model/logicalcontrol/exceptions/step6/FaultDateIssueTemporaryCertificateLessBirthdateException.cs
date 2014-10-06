// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDateIssueTemporaryCertificateLessBirthdateException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault date issue temporary certificate less birthdate exception.
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
  ///   The fault date issue temporary certificate less birthdate exception.
  /// </summary>
  [Serializable]
  public class FaultDateIssueTemporaryCertificateLessBirthdateException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDateIssueTemporaryCertificateLessBirthdateException" /> class.
    /// </summary>
    public FaultDateIssueTemporaryCertificateLessBirthdateException()
      : base(
        new ExceptionInfo(Resource.FaultDateIssueTemporaryCertificateLessBirthdateCode), 
        Resource.FaultDateIssueTemporaryCertificateLessBirthdateMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateIssueTemporaryCertificateLessBirthdateException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateIssueTemporaryCertificateLessBirthdateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}