// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseRenevalLoss.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The cause reneval loss.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reneval loss.
  /// </summary>
  public class CauseRenevalLoss : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseRenevalLoss" /> class.
    /// </summary>
    public CauseRenevalLoss()
      : base(CauseReneval.RenevalLoss)
    {
    }

    #endregion
  }
}