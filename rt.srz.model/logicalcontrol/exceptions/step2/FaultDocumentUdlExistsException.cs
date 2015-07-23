// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDocumentUdlExistsException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault document udl exists exception.
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
  ///   The fault document udl exists exception.
  /// </summary>
  [Serializable]
  public class FaultDocumentUdlExistsException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentUdlExistsException" /> class.
    /// </summary>
    public FaultDocumentUdlExistsException()
      : base(
        new ExceptionInfo(Resource.FaultDocumentUdlExistsExceptionCode), 
        Resource.FaultDocumentUdlExistsExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentUdlExistsException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentUdlExistsException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}