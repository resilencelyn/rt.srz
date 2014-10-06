// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultSignatureFormatException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault signature format exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step5
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault signature format exception.
  /// </summary>
  [Serializable]
  public class FaultSignatureFormatException : FaultStep5
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultSignatureFormatException" /> class.
    /// </summary>
    public FaultSignatureFormatException()
      : base(
        new ExceptionInfo(Resource.FaultSignatureFormatExceptionCode), 
        Resource.FaultSignatureFormatExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultSignatureFormatException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultSignatureFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}