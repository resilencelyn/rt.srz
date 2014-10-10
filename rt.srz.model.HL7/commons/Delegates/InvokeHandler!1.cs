// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvokeHandler!1.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The invoke handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Delegates
{
  /// <summary>
  /// The invoke handler.
  /// </summary>
  /// <param name="parameters">
  /// The parameters.
  /// </param>
  /// <typeparam name="T">
  /// </typeparam>
  public delegate T InvokeHandler<T>(params object[] parameters);
}