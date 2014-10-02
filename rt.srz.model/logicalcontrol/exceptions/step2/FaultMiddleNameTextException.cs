﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultMiddleNameTextException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
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
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultMiddleNameTextException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultMiddleNameTextException" /> class.
    /// </summary>
    public FaultMiddleNameTextException()
      : base(new ExceptionInfo(Resource.FaultMiddleNameTextExceptionCode), Resource.FaultMiddleNameTextExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultMiddleNameTextException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultMiddleNameTextException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}