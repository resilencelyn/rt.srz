// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultGenderConformityFirstNameException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault gender conformity exception.
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
  /// The fault gender conformity exception.
  /// </summary>
  [Serializable]
  public class FaultGenderConformityFirstNameException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultGenderConformityFirstNameException"/> class.
    /// </summary>
    public FaultGenderConformityFirstNameException()
      : base(
        new ExceptionInfo(Resource.FaultGenderConformityExceptionCode), Resource.FaultGenderConformityExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultGenderConformityFirstNameException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultGenderConformityFirstNameException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}