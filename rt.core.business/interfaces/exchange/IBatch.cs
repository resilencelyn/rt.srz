// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBatch.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.business.interfaces.exchange
{
  #region

  using System;

  #endregion

  /// <summary>
  /// The Batch interface.
  /// </summary>
  public interface IBatch
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    string FileName { get; set; }

    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the number.
    /// </summary>
    short Number { get; set; }

    #endregion
  }
}