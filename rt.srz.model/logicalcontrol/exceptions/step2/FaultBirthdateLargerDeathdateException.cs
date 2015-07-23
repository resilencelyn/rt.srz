// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultBirthdateLargerDeathdateException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birthdate larger deathdate exception.
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
  ///   The fault birthdate larger deathdate exception.
  /// </summary>
  [Serializable]
  public class FaultBirthdateLargerDeathdateException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultBirthdateLargerDeathdateException" /> class.
    /// </summary>
    public FaultBirthdateLargerDeathdateException()
      : base(
        new ExceptionInfo(Resource.FaultBirthdateLargerDeathdateExceptionCode), 
        Resource.FaultBirthdateLargerDeathdateExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateLargerDeathdateException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthdateLargerDeathdateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}