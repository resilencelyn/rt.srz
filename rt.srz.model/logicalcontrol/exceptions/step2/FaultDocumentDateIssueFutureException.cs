// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDocumentDateIssueFutureException.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
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
  public class FaultDocumentDateIssueFutureException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentDateIssueFutureException" /> class.
    /// </summary>
    public FaultDocumentDateIssueFutureException()
      : base(
        new ExceptionInfo(Resource.FaultDocumentDateIssueFutureExceptionCode), 
        Resource.FaultDocumentDateIssueFutureExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentDateIssueFutureException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentDateIssueFutureException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}