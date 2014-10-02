// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultNewEnpBirthdayAndGenderException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault enp birthday and gender exception.
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
  /// The fault new enp birthday and gender exception.
  /// </summary>
  [Serializable]
  public class FaultNewEnpBirthdayAndGenderException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultNewEnpBirthdayAndGenderException"/> class.
    /// </summary>
    public FaultNewEnpBirthdayAndGenderException() : base(new ExceptionInfo(Resource.FaultEnpBirthdayAndGenderExceptionCode),
        Resource.FaultEnpBirthdayAndGenderExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultNewEnpBirthdayAndGenderException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultNewEnpBirthdayAndGenderException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}