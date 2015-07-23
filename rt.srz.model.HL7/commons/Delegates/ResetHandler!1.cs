// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResetHandler!1.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The reset handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Delegates
{
  /// <summary>
  /// The reset handler.
  /// </summary>
  /// <param name="value">
  /// The value.
  /// </param>
  /// <typeparam name="T">
  /// </typeparam>
  public delegate void ResetHandler<T>(ref T value);
}