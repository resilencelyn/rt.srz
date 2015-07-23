// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultIssue.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault issue.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6.issue
{
  using System.Runtime.Serialization;

  /// <summary>
  /// The fault issue.
  /// </summary>
  public class FaultIssue : FaultStep6
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultIssue"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public FaultIssue(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultIssue"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public FaultIssue(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    #endregion

    #region Methods

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

    #endregion
  }
}