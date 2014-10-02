// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReinsuranceWithTheMove.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The cause reinsurance with the move.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reinsurance
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reinsurance with the move.
  /// </summary>
  public class CauseReinsuranceWithTheMove : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseReinsuranceWithTheMove" /> class.
    /// </summary>
    public CauseReinsuranceWithTheMove()
      : base(CauseReinsurance.ReinsuranceWithTheMove)
    {
    }

    #endregion
  }
}