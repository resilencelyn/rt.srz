// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FaultStep5.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The fault step 5.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol.exceptions.step5
{
  using System.Runtime.Serialization;

  /// <summary>
  /// The fault step 5.
  /// </summary>
  public class FaultStep5 : LogicalControlException
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep5"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="message">
    /// The message.
    /// </param>
    public FaultStep5(ExceptionInfo info, string message)
      : base(info, message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FaultStep5"/> class.
    /// </summary>
    /// <param name="info">
    /// The info.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    public FaultStep5(SerializationInfo info, StreamingContext context)
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
      return 5;
    }

    #endregion
  }
}