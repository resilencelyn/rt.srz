// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultStep6.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault step 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step6
{
  using System.Runtime.Serialization;

  /// <summary>
  /// The fault step 6.
  /// </summary>
  public class FaultStep6 : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep6"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public FaultStep6(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep6"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public FaultStep6(SerializationInfo info, StreamingContext context)
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