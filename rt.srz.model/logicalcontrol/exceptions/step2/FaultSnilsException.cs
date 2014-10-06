// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultSnilsException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
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
  public class FaultSnilsException : FaultStep2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="FaultSnilsException" /> class.
    /// </summary>
    public FaultSnilsException()
      : base(new ExceptionInfo(Resource.FaultSnilsExceptionCode), Resource.FaultSnilsExceptionMewssage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultSnilsException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    protected FaultSnilsException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion
  }
}