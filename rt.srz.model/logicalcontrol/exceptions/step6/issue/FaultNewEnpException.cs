// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEnpException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
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
  ///  The fault new enp exception.
  /// </summary>
  [Serializable]
  public class FaultNewEnpException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultNewEnpException" /> class.
    /// </summary>
    public FaultNewEnpException()
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
    protected FaultNewEnpException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
    #endregion
  }
}