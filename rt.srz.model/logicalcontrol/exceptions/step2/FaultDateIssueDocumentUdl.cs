// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultDateIssueDocumentUdl.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault birth certificate exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  ///   The fault birth certificate exception.
  /// </summary>
  [Serializable]
  public class FaultDateIssueDocumentUdl : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultDateIssueDocumentUdl" /> class.
    /// </summary>
    public FaultDateIssueDocumentUdl()
      : base(new ExceptionInfo(Resource.FaultDateIssueDocumentUdlCode), Resource.FaultDateIssueDocumentUdlMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultDateIssueDocumentUdl"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultDateIssueDocumentUdl(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}