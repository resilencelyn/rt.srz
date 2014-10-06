// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultBirthCertificateException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birth certificate exception.
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
  ///   The fault birth certificate exception.
  /// </summary>
  [Serializable]
  public class FaultBirthCertificateException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultBirthCertificateException" /> class.
    /// </summary>
    public FaultBirthCertificateException()
      : base(
        new ExceptionInfo(Resource.FaultBirthCertificateExceptionCode), 
        Resource.FaultBirthCertificateExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthCertificateException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthCertificateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}