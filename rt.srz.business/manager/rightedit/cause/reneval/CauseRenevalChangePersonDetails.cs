// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseRenevalChangePersonDetails.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reneval change person details.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reneval change person details.
  /// </summary>
  public class CauseRenevalChangePersonDetails : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseRenevalChangePersonDetails" /> class.
    /// </summary>
    public CauseRenevalChangePersonDetails()
      : base(CauseReneval.RenevalChangePersonDetails)
    {
    }

    #endregion
  }
}