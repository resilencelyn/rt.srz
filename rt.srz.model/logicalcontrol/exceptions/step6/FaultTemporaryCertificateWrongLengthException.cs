// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateWrongLengthException.cs" company="Альянс">
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
  public class FaultTemporaryCertificateWrongLengthException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateWrongLengthException" /> class.
    /// </summary>
    public FaultTemporaryCertificateWrongLengthException()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateWrongLengthExceptionCode), 
        Resource.FaultTemporaryCertificateWrongLengthExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateWrongLengthException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateWrongLengthException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}