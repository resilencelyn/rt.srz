// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageP03.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message p 01.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  /// <summary>
  ///   The message p03.
  /// </summary>
  public class MessageP03 : MessageA08Ins2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MessageP03" /> class.
    /// </summary>
    public MessageP03()
      : base(srz.model.srz.concepts.ReasonType.П03, "П03")
    {
    }

    #endregion
  }
}