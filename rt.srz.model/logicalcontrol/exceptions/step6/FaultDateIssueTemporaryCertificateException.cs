// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDateIssueTemporaryCertificateException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault date issue temporary certificate exception.
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
  /// The fault date issue temporary certificate exception.
  /// </summary>
  [Serializable]
  public class FaultDateIssueTemporaryCertificateException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateIssueTemporaryCertificateException"/> class.
    /// </summary>
    public FaultDateIssueTemporaryCertificateException()
      : base(
        new ExceptionInfo(Resource.FaultDateIssueTemporaryCertificateCode), 
        Resource.FaultDateIssueTemporaryCertificateMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateIssueTemporaryCertificateException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateIssueTemporaryCertificateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}