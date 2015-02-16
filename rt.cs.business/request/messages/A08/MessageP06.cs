// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageP06.cs" company="��������">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message p 01.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.cs.business.request.messages.A08
{
  /// <summary>
  ///   The message p06.
  /// </summary>
  public class MessageP06 : MessageA08Ins2
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="MessageP06" /> class.
    /// </summary>
    public MessageP06()
      : base(srz.model.srz.concepts.ReasonType.�06, "�06")
    {
    }

    #endregion
  }
}