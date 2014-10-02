// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultTemporaryCertificateFormatException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault temporary certificate format exception.
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
  /// The fault temporary certificate format exception.
  /// </summary>
  [Serializable]
  public class FaultTemporaryCertificateFormatException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateFormatException"/> class.
    /// </summary>
    public FaultTemporaryCertificateFormatException()
      : base(
        new ExceptionInfo(Resource.FaultTemporaryCertificateFormatExceptionCode), 
        Resource.FaultTemporaryCertificateFormatExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultTemporaryCertificateFormatException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultTemporaryCertificateFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}