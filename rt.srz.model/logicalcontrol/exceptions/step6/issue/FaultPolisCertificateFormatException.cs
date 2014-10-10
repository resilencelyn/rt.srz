// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPolisCertificateFormatException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault polis certificate format exception.
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
  ///   The fault polis certificate format exception.
  /// </summary>
  [Serializable]
  public class FaultPolisCertificateFormatException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPolisCertificateFormatException" /> class.
    /// </summary>
    public FaultPolisCertificateFormatException()
      : base(
        new ExceptionInfo(Resource.FaultPolisCertificateFormatExceptionCode), 
        Resource.FaultPolisCertificateFormatExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPolisCertificateFormatException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPolisCertificateFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}