// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEnpExistsException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault enp exists exception.
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
  ///   The fault enp exists exception.
  /// </summary>
  [Serializable]
  public class FaultEnpExistsException : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEnpExistsException" /> class.
    /// </summary>
    public FaultEnpExistsException()
      : base(new ExceptionInfo(Resource.FaultEnpExistsExceptionCode), Resource.FaultEnpExistsExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEnpExistsException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEnpExistsException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}