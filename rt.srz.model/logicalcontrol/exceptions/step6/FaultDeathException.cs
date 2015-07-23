// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDeathException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault death exception.
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
  ///   The fault death exception.
  /// </summary>
  [Serializable]
  public class FaultDeathException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDeathException" /> class.
    /// </summary>
    public FaultDeathException()
      : base(new ExceptionInfo(Resource.FaultDeathExceptionCode), Resource.FaultDeathExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDeathException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDeathException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}