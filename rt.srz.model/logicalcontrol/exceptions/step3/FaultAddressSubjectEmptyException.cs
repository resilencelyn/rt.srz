// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultAddressSubjectEmptyException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault address region exception.
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
  ///   The fault address region exception.
  /// </summary>
  [Serializable]
  public class FaultAddressSubjectEmptyException : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultAddressSubjectEmptyException" /> class.
    ///   Initializes a new instance of the <see cref="FaultAddressRegionEmptyException" /> class.
    /// </summary>
    public FaultAddressSubjectEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultAddressSubjectEmptyExceptionCode), 
        Resource.FaultAddressSubjectEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultAddressSubjectEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultAddressSubjectEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}