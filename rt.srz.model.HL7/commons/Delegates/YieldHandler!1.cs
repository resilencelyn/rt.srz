// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YieldHandler!1.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The yield handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Delegates
{
  /// <summary>
  /// The yield handler.
  /// </summary>
  /// <param name="value">
  /// The value.
  /// </param>
  /// <typeparam name="T">
  /// </typeparam>
  public delegate T YieldHandler<T>(T value);
}