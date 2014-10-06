// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultAddressNotComplete.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault address not complete.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step3
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  /// The fault address not complete.
  /// </summary>
  [Serializable]
  public class FaultAddressNotComplete : FaultStep3
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultAddressNotComplete"/> class.
    /// </summary>
    public FaultAddressNotComplete()
      : base(
        new ExceptionInfo(Resource.FaultAddressNotCompleteExceptionCode), 
        Resource.FaultAddressNotCompleteExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultAddressNotComplete"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultAddressNotComplete(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}