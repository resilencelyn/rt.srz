// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultPolicyTypeException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
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
  public class FaultPolicyTypeException : FaultIssue
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultPolicyTypeException" /> class.
    /// </summary>
    public FaultPolicyTypeException()
      : base(new ExceptionInfo(Resource.FaultPolicyTypeExceptionCode), Resource.FaultPolicyTypeExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultPolicyTypeException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultPolicyTypeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}