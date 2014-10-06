// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPostcodeException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault postcode exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault postcode exception.
  /// </summary>
  [Serializable]
  public class FaultPostcodeException : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPostcodeException" /> class.
    /// </summary>
    public FaultPostcodeException()
      : base(new ExceptionInfo(Resource.FaultPostcodeExceptionCode), Resource.FaultPostcodeExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPostcodeException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPostcodeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}