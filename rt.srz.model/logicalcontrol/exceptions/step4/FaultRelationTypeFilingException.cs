// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultRelationTypeFilingException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault relation type exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step4
{
  #region

  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  #endregion

  /// <summary>
  ///   The fault relation type exception.
  /// </summary>
  [Serializable]
  public class FaultRelationTypeFilingException : FaultStep4
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultRelationTypeFilingException" /> class.
    /// </summary>
    public FaultRelationTypeFilingException()
      : base(new ExceptionInfo(Resource.FaultRelationTypeExceptionCode), Resource.FaultRelationTypeExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultRelationTypeFilingException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultRelationTypeFilingException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}