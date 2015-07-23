// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentPatternException.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The document pattern exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.database.business.standard
{
  using System;

  /// <summary>
  /// The document pattern exception.
  /// </summary>
  public class DocumentPatternException : Exception
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentPatternException"/> class.
    /// </summary>
    /// <param name="message">
    /// The message.
    /// </param>
    public DocumentPatternException(string message)
      : base(message)
    {
    }

    #endregion
  }
}