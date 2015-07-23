// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageP01.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message p 01.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  /// <summary>
  ///   The message p 01.
  /// </summary>
  public class MessageP01 : MessageA08Ins1
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MessageP01" /> class.
    /// </summary>
    public MessageP01()
      : base(srz.model.srz.concepts.ReasonType.П01,  "П01")
    {
    }

    #endregion
  }
}