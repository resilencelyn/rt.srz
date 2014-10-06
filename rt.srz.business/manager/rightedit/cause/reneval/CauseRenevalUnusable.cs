// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseRenevalUnusable.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reneval unusable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reneval unusable.
  /// </summary>
  public class CauseRenevalUnusable : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseRenevalUnusable" /> class.
    /// </summary>
    public CauseRenevalUnusable()
      : base(CauseReneval.RenevalUnusable)
    {
    }

    #endregion
  }
}