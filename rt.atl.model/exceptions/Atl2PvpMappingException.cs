// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Atl2PvpMappingException.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The atl 2 pvp mapping exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.exceptions
{
  using System;

  /// <summary>
  /// The atl 2 pvp mapping exception.
  /// </summary>
  public class Atl2PvpMappingException : Exception
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Atl2PvpMappingException"/> class.
    /// </summary>
    /// <param name="innerException">
    /// The inner exception.
    /// </param>
    public Atl2PvpMappingException(Exception innerException = null)
      : base("Ошибка мапинга заявления из Атлантики в ПВП ", innerException)
    {
    }

    #endregion
  }
}