// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReinsuranceAtWill.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reinsurance at will.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reinsurance
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reinsurance at will.
  /// </summary>
  public class CauseReinsuranceAtWill : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseReinsuranceAtWill" /> class.
    /// </summary>
    public CauseReinsuranceAtWill()
      : base(CauseReinsurance.ReinsuranceAtWill)
    {
    }

    #endregion
  }
}