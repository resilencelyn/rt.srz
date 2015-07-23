// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPolisCertificateEmptyException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
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
  public class FaultPolisCertificateEmptyException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPolisCertificateEmptyException" /> class.
    /// </summary>
    public FaultPolisCertificateEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultPolisCertificateEmptyExceptionCode), 
        Resource.FaultPolisCertificateEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPolisCertificateEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPolisCertificateEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}