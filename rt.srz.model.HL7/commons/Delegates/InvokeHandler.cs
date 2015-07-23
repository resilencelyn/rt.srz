// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvokeHandler.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The invoke handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Delegates
{
  /// <summary>
  ///   The invoke handler.
  /// </summary>
  /// <param name="parameters">
  ///   The parameters.
  /// </param>
  public delegate object InvokeHandler(params object[] parameters);
}