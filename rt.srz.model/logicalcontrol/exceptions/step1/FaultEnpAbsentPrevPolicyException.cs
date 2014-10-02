// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEnpAbsentPrevPolicyException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step1
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.barcode.Properties;

  #endregion

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultEnpAbsentPrevPolicyException : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEnpAbsentPrevPolicyException" /> class.
    /// </summary>
    public FaultEnpAbsentPrevPolicyException()
      : base(
        new ExceptionInfo(Resource.FaultEnpAbsentPrevPolicyExceptionCode), 
        Resource.FaultEnpAbsentPrevPolicyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEnpAbsentPrevPolicyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEnpAbsentPrevPolicyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
    #endregion
  }
}