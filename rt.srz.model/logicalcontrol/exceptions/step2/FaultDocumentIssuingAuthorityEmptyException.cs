// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDocumentIssuingAuthorityEmptyException.cs" company="Альянс">
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
  public class FaultDocumentIssuingAuthorityEmptyException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentIssuingAuthorityEmptyException" /> class.
    /// </summary>
    public FaultDocumentIssuingAuthorityEmptyException()
      : base(
        new ExceptionInfo(Resource.FaultDocumentIssuingAuthorityEmptyExceptionCode), 
        Resource.FaultDocumentIssuingAuthorityEmptyExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentIssuingAuthorityEmptyException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentIssuingAuthorityEmptyException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}