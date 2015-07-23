// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchTimeoutException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search timeout exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.logicalcontrol
{
  using System;

  /// <summary>
  /// The search timeout exception.
  /// </summary>
  [Serializable]
  public class SearchTimeoutException : Exception
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchTimeoutException"/> class. 
    /// Конструктор
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    public SearchTimeoutException(string message, Exception e)
      : base(message)
    {
    }

    #endregion
  }
}