// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageP02.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message p 01.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  /// <summary>
  ///   The message p02.
  /// </summary>
  public class MessageP02 : MessageA08Ins1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MessageP02" /> class.
    /// </summary>
    public MessageP02()
      : base(srz.model.srz.concepts.ReasonType.П02, "П02")
    {
    }

    #endregion
  }
}