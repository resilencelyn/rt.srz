// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPseudonymizationServiceCallException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault pseudonymization service call exception.
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
  ///   The fault pseudonymization service call exception.
  /// </summary>
  [Serializable]
  public class FaultPseudonymizationServiceCallException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPseudonymizationServiceCallException" /> class.
    /// </summary>
    public FaultPseudonymizationServiceCallException()
      : base(
        new ExceptionInfo(Resource.FaultPseudonymizationServiceCallExceptionCode), 
        Resource.FaultPseudonymizationServiceCallExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPseudonymizationServiceCallException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPseudonymizationServiceCallException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}