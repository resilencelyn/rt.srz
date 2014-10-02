// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEmptyPhotoException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault empty photo exception.
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
  /// The fault empty photo exception.
  /// </summary>
  [Serializable]
  public class FaultEmptyPhotoException : FaultStep5
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmptyPhotoException"/> class.
    /// </summary>
    public FaultEmptyPhotoException()
      : base(new ExceptionInfo(Resource.FaultEmptyPhotoExceptionCode), Resource.FaultEmptyPhotoExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmptyPhotoException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEmptyPhotoException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}