// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultGenderException.cs" company="Альянс">
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
  public class FaultGenderException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultGenderException" /> class.
    /// </summary>
    public FaultGenderException()
      : base(new ExceptionInfo(Resource.FaultGenderExceptionCode), Resource.FaultGenderExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultGenderException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultGenderException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}