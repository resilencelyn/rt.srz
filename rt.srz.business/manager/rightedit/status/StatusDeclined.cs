// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusDeclined.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The status declined.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.status
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The status declined.
  /// </summary>
  public class StatusDeclined : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusDeclined" /> class.
    /// </summary>
    public StatusDeclined()
      : base(StatusStatement.Declined)
    {
    }

    #endregion
  }
}