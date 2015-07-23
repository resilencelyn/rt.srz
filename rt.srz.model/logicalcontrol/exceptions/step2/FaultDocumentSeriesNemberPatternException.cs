// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDocumentSeriesNemberPatternException.cs" company="Альянс">
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
  public class FaultDocumentSeriesNemberPatternException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentSeriesNemberPatternException" /> class.
    /// </summary>
    public FaultDocumentSeriesNemberPatternException()
      : base(new ExceptionInfo(Resource.FaultDocumentExceptionCode), Resource.FaultDocumentExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentSeriesNemberPatternException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentSeriesNemberPatternException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}