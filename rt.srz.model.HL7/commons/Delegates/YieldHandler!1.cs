// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YieldHandler!1.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The yield handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Delegates
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