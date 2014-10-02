// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultFirstNameTextException.cs" company="Rintech">
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
  public class FaultFirstNameTextException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultFirstNameTextException" /> class.
    /// </summary>
    public FaultFirstNameTextException()
      : base(new ExceptionInfo(Resource.FaultFirstNameTextExceptionCode), Resource.FaultFirstNameTextExceptionMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultFirstNameTextException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultFirstNameTextException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}