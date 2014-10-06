// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultThereAreUnclosedStatementsException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault snils exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step1
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
  public class FaultThereAreUnclosedStatementsException : FaultStep1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultThereAreUnclosedStatementsException" /> class.
    /// </summary>
    public FaultThereAreUnclosedStatementsException()
      : base(
        new ExceptionInfo(Resource.FaultThereAreUnclosedStatementsExceptionCode), 
        Resource.FaultThereAreUnclosedStatementsExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultThereAreUnclosedStatementsException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultThereAreUnclosedStatementsException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}