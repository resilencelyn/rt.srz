// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPhotoFormatException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault photo format exception.
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
  ///   The fault photo format exception.
  /// </summary>
  [Serializable]
  public class FaultPhotoFormatException : FaultStep5
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPhotoFormatException" /> class.
    /// </summary>
    public FaultPhotoFormatException()
      : base(new ExceptionInfo(Resource.FaultPhotoFormatExceptionCode), Resource.FaultPhotoFormatExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPhotoFormatException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPhotoFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}