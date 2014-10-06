// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseReinsuranceStopFinance.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause reinsurance stop finance.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reinsurance
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause reinsurance stop finance.
  /// </summary>
  public class CauseReinsuranceStopFinance : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseReinsuranceStopFinance" /> class.
    /// </summary>
    public CauseReinsuranceStopFinance()
      : base(CauseReinsurance.ReinsuranceStopFinance)
    {
    }

    #endregion
  }
}