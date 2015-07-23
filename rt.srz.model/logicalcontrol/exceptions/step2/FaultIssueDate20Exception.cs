// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultIssueDate20Exception.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault issue date 20 exception.
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
  ///   The fault issue date 20 exception.
  /// </summary>
  [Serializable]
  public class FaultIssueDate20Exception : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultIssueDate20Exception" /> class.
    /// </summary>
    public FaultIssueDate20Exception()
      : base(new ExceptionInfo(Resource.FaultIssueDate20ExceptionCode), Resource.FaultIssueDate20ExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultIssueDate20Exception"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultIssueDate20Exception(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}