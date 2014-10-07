// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldLengthException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The field length exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.Properties;

  /// <summary>
  ///   The field length exception.
  /// </summary>
  [Serializable]
  public class FieldLengthException : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldLengthException"/> class.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    public FieldLengthException(string fieldName)
      : base(
        new ExceptionInfo(Resource.FaultFieldLengthExceptionCode), 
        string.Format(Resource.FaultFieldLengthExceptionMessage, fieldName))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FieldLengthException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public FieldLengthException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

    #region Methods

    /// <summary>
    ///   The step.
    /// </summary>
    /// <returns>
    ///   The <see cref="int" />.
    /// </returns>
    protected override int Step()
    {
      return 6;
    }

    #endregion
  }
}