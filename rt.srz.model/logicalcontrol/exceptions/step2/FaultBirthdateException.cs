// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultBirthdateException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birthdate exception.
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
  ///   The fault birthdate exception.
  /// </summary>
  [Serializable]
  public class FaultBirthdateException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultBirthdateException" /> class.
    /// </summary>
    public FaultBirthdateException()
      : base(new ExceptionInfo(Resource.FaultBirthdateExceptionCode), Resource.FaultBirthdateExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultBirthdateException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultBirthdateException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}