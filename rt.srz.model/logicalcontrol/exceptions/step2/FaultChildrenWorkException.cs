// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultChildrenWorkException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault children work exception.
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
  ///   The fault children work exception.
  /// </summary>
  [Serializable]
  public class FaultChildrenWorkException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultChildrenWorkException" /> class.
    /// </summary>
    public FaultChildrenWorkException()
      : base(new ExceptionInfo(Resource.FaultChildrenWorkExceptionCode), Resource.FaultChildrenWorkExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultChildrenWorkException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultChildrenWorkException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}