// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseRenevalInaccuracy.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reneval inaccuracy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reneval inaccuracy.
  /// </summary>
  public class CauseRenevalInaccuracy : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseRenevalInaccuracy" /> class.
    /// </summary>
    public CauseRenevalInaccuracy()
      : base(CauseReneval.RenevalInaccuracy)
    {
    }

    #endregion
  }
}