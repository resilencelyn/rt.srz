// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultCitizenshipException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
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
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultCitizenshipException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultCitizenshipException" /> class.
    /// </summary>
    public FaultCitizenshipException()
      : base(new ExceptionInfo(Resource.FaultCitizenshipExceptionCode), Resource.FaultCitizenshipExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultCitizenshipException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultCitizenshipException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}