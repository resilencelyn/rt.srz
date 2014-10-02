// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchException.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Defines the SearchException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions
{
  using System;
  using System.Runtime.Serialization;

  using rt.srz.model.barcode.Properties;

  [Serializable]
  public class SearchException: LogicalControlException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public SearchException(string message)
      : base(new ExceptionInfo("117"), message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchException"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public SearchException(SerializationInfo info, StreamingContext context)
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
      return 1;
    }
  }
}