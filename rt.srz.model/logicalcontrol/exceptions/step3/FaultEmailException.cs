// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEmailException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault email exception.
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
  /// The fault email exception.
  /// </summary>
  [Serializable]
  public class FaultEmailException : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmailException"/> class.
    /// </summary>
    public FaultEmailException()
      : base(new ExceptionInfo(Resource.FaultEmailExceptionCode), Resource.FaultEmailExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmailException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEmailException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

   }
}