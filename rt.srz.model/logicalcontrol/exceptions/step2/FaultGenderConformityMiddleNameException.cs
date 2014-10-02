﻿namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  using Resource = rt.srz.model.barcode.Properties.Resource;

  /// <summary>
  /// The fault gender conformity exception.
  /// </summary>
  [Serializable]
  public class FaultGenderConformityMiddleNameException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultGenderConformityMiddleNameException"/> class.
    /// </summary>
    public FaultGenderConformityMiddleNameException()
      : base(
        new ExceptionInfo(Resource.FaultGenderConformityMiddleNameExceptionCode), Resource.FaultGenderConformityMiddleNameExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultGenderConformityMiddleNameException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultGenderConformityMiddleNameException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}