// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultCauseFilingException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultCauseFilingException : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultCauseFilingException" /> class.
    /// </summary>
    public FaultCauseFilingException()
      : base(new ExceptionInfo(Resource.FaultCauseFilingExceptionCode), Resource.FaultCauseFilingExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultCauseFilingException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultCauseFilingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}