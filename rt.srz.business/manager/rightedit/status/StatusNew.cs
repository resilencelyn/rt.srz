// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusNew.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The status new.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.manager.rightedit.status
{
  #region references

  using rt.srz.model.srz.concepts;

  #endregion

  /// <summary>
  ///   The status new.
  /// </summary>
  public class StatusNew : StatementRightToEdit
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="StatusNew" /> class.
    /// </summary>
    public StatusNew()
      : base(StatusStatement.New)
    {
    }

    #endregion
  }
}