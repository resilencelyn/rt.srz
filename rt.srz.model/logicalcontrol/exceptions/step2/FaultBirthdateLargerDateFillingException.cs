// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultBirthdateLargerDateFillingException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birthdate larger document date issue exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  ///   The fault birthdate larger document date issue exception.
  /// </summary>
  [Serializable]
  public class FaultBirthdateLargerDateFillingException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultBirthdateLargerDateFillingException" /> class.
    /// </summary>
    public FaultBirthdateLargerDateFillingException()
      : base(
        new ExceptionInfo(Resource.FaultBirthdateLargerDateFillingExceptionCode), 
        Resource.FaultBirthdateLargerDateFillingExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateLargerDateFillingException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthdateLargerDateFillingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}