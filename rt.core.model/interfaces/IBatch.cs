// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBatch.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The Batch interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.interfaces
{
  #region

  using System;

  #endregion

  /// <summary>
  ///   The Batch interface.
  /// </summary>
  public interface IBatch
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the file name.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets the number.
    /// </summary>
    short Number { get; set; }

    #endregion
  }
}