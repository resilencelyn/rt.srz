// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultStep2.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault step 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step2
{
  using System.Runtime.Serialization;

  /// <summary>
  /// The fault step 2.
  /// </summary>
  public class FaultStep2 : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep2"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public FaultStep2(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep2"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public FaultStep2(SerializationInfo info, StreamingContext context)
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
      return 2;
    }

    #endregion
  }
}