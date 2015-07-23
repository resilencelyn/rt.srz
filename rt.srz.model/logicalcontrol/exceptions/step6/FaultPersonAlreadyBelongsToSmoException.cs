// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPersonAlreadyBelongsToSmoException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault person already belongs to smo exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.logicalcontrol.exceptions.step2;
  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault person already belongs to smo exception.
  /// </summary>
  [Serializable]
  public class FaultPersonAlreadyBelongsToSmoException : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPersonAlreadyBelongsToSmoException" /> class.
    ///   Initializes a new instance of the <see cref="FaultMiddleNameTextException" /> class.
    /// </summary>
    public FaultPersonAlreadyBelongsToSmoException()
      : base(
        new ExceptionInfo(Resource.FaultPersonAlreadyBelongsToSmoExceptionCode), 
        Resource.FaultPersonAlreadyBelongsToSmoExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPersonAlreadyBelongsToSmoException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPersonAlreadyBelongsToSmoException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}