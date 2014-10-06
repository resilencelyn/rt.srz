// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateNumberInRangeException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault temporary certificate number in range exception.
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
  ///   The fault temporary certificate number in range exception.
  /// </summary>
  [Serializable]
  public class FaultTemporaryCertificateNumberInRangeException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultTemporaryCertificateNumberInRangeException" /> class.
    /// </summary>
    public FaultTemporaryCertificateNumberInRangeException()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateNumberInRangeExceptionCode), 
        Resource.FaultTemporaryCertificateNumberInRangeExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateNumberInRangeException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateNumberInRangeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}