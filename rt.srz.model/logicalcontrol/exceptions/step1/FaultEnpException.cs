// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEnpException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step1
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
  public class FaultEnpException : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEnpException" /> class.
    /// </summary>
    public FaultEnpException()
      : base(new ExceptionInfo(Resource.FaultEnpExceptionCode), Resource.FaultEnpExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEnpException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEnpException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
    #endregion
  }
}