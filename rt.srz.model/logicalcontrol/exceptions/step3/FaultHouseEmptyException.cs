// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultHouseEmptyException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault house empty exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault house empty exception.
  /// </summary>
  [Serializable]
  public class FaultHouseEmptyException : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultHouseEmptyException"/> class. 
    ///   Initializes a new instance of the <see cref="FaultPostcodeException"/> class.
    /// </summary>
    public FaultHouseEmptyException()
      : base(new ExceptionInfo(Resource.FaultHouseEmptyExceptionCode), Resource.FaultHouseEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultHouseEmptyException"/> class. 
    /// Initializes a new instance of the <see cref="FaultPostcodeException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultHouseEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}