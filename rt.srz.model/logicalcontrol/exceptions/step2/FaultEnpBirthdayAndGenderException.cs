// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEnpBirthdayAndGenderException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault enp birthday and gender exception.
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
  ///   The fault enp birthday and gender exception.
  /// </summary>
  [Serializable]
  public class FaultEnpBirthdayAndGenderException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEnpBirthdayAndGenderException" /> class.
    /// </summary>
    public FaultEnpBirthdayAndGenderException()
      : base(
        new ExceptionInfo(Resource.FaultEnpBirthdayAndGenderExceptionCode), 
        Resource.FaultEnpBirthdayAndGenderExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEnpBirthdayAndGenderException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEnpBirthdayAndGenderException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}