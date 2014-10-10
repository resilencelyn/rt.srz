// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPolisCertificateWrongLengthException.cs" company="РусБИТех">
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
  public class FaultPolisCertificateWrongLengthException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPolisCertificateWrongLengthException" /> class.
    /// </summary>
    public FaultPolisCertificateWrongLengthException()
      : base(
        new ExceptionInfo(Resource.FaultPolisCertificateWrongLengthExceptionCode), 
        Resource.FaultPolisCertificateWrongLengthExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPolisCertificateWrongLengthException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPolisCertificateWrongLengthException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}