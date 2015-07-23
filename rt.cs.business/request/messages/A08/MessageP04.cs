// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageP04.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message p 01.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  /// <summary>
  ///   The message p04.
  /// </summary>
  public class MessageP04 : MessageA08Ins1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MessageP04" /> class.
    /// </summary>
    public MessageP04()
      : base(srz.model.srz.concepts.ReasonType.П04,"П04")
    {
    }

    #endregion
  }
}