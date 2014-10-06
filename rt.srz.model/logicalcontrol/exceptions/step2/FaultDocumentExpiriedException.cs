// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDocumentExpiriedException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  ///   The fault snils exception.
  /// </summary>
  [Serializable]
  public class FaultDocumentExpiriedException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDocumentExpiriedException" /> class.
    /// </summary>
    public FaultDocumentExpiriedException()
      : base(new ExceptionInfo("99"), "Документ уже не действует")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDocumentExpiriedException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDocumentExpiriedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}