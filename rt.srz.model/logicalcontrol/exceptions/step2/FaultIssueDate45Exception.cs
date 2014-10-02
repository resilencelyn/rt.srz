// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultIssueDate45Exception.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The fault issue date 45 exception.
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
  /// The fault issue date 45 exception.
  /// </summary>
  [Serializable]
  public class FaultIssueDate45Exception : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultIssueDate45Exception"/> class.
    /// </summary>
    public FaultIssueDate45Exception()
      : base(new ExceptionInfo(Resource.FaultIssueDate45ExceptionCode), Resource.FaultIssueDate45ExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultIssueDate45Exception"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultIssueDate45Exception(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

   #endregion
  }
}