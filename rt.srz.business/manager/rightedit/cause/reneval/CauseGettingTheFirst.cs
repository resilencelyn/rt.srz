// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CauseGettingTheFirst.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cause getting the first.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.cause.reneval
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The cause getting the first.
  /// </summary>
  public class CauseGettingTheFirst : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CauseGettingTheFirst" /> class.
    /// </summary>
    public CauseGettingTheFirst()
      : base(CauseReneval.GettingTheFirst)
    {
    }

    #endregion
  }
}