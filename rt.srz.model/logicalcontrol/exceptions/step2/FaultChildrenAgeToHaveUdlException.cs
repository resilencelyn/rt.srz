// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultChildrenAgeToHaveUdlException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault children age to have udl exception.
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
  /// The fault children age to have udl exception.
  /// </summary>
  [Serializable]
  public class FaultChildrenAgeToHaveUdlException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultChildrenAgeToHaveUdlException"/> class.
    /// </summary>
    public FaultChildrenAgeToHaveUdlException()
      : base(
        new ExceptionInfo(Resource.FaultChildrenAgeToHaveUdlExceptionCode), 
        Resource.FaultChildrenAgeToHaveUdlExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultChildrenAgeToHaveUdlException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultChildrenAgeToHaveUdlException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}