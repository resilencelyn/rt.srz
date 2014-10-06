// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions
{
  using System;
  using System.Runtime.Serialization;

  /// <summary>
  /// The search exception.
  /// </summary>
  [Serializable]
  public class SearchException : LogicalControlException
  {
    #region Constructors and Destructors

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

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the info.
    /// </summary>
    [DataMember]
    public ExceptionInfo Info { get; set; }

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
      return 1;
    }

    #endregion
  }
}