// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseChoice.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The cause choice.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reinsurance
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause choice.
  /// </summary>
  public class CauseChoice : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseChoice" /> class.
    /// </summary>
    public CauseChoice()
      : base(CauseReinsurance.Choice)
    {
    }

    #endregion
  }
}