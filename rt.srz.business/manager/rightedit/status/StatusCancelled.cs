// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusCancelled.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The status cancelled.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.status
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The status cancelled.
  /// </summary>
  public class StatusCancelled : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusCancelled" /> class.
    /// </summary>
    public StatusCancelled()
      : base(StatusStatement.Cancelled)
    {
    }

    #endregion
  }
}