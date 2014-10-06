// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateEmptyException.cs" company="РусБИТех">
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
  public class FaultTemporaryCertificateEmptyException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateEmptyException" /> class.
    /// </summary>
    public FaultTemporaryCertificateEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateEmptyExceptionCode), 
        Resource.FaultTemporaryCertificateEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}