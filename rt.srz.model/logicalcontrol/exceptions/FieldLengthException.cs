// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FieldLengthException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The field length exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

using rt.srz.model.Properties;

namespace rt.srz.model.logicalcontrol.exceptions
{
  /// <summary>
  /// The field length exception.
  /// </summary>
  [Serializable]
  public class FieldLengthException : LogicalControlException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="FieldLengthException"/> class.
    /// </summary>
    /// <param name="fieldName">
    /// The field name.
    /// </param>
    public FieldLengthException(string fieldName)
      : base(new ExceptionInfo(Resource.FaultFieldLengthExceptionCode), string.Format(Resource.FaultFieldLengthExceptionMessage, fieldName))
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

    /// <summary>
    ///   Gets or sets the info.
    /// </summary>
    [DataMember]
    public ExceptionInfo Info { get; set; }

    /// <summary>
    /// The step.
    /// </summary>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    protected override int Step()
    {
      return 6;
    }
  }
}
