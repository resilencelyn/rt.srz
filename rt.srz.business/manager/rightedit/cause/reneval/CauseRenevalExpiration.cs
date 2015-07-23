// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseRenevalExpiration.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reneval expiration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reneval expiration.
  /// </summary>
  public class CauseRenevalExpiration : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseRenevalExpiration" /> class.
    /// </summary>
    public CauseRenevalExpiration()
      : base(CauseReneval.RenevalExpiration)
    {
    }

    #endregion
  }
}