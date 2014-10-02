// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultEmptyDocumentException.cs" company="Rintech">
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
  public class FaultEmptyDocumentException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultEmptyDocumentException" /> class.
    /// </summary>
    public FaultEmptyDocumentException()
      : base(new ExceptionInfo(Resource.FaultEmptyDocumentExceptionCode), Resource.FaultEmptyDocumentExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultEmptyDocumentException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultEmptyDocumentException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

   }
}