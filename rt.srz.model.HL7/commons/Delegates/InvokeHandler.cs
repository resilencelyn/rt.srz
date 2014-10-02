// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvokeHandler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The invoke handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Delegates
{
  /// <summary>
  ///   The invoke handler.
  /// </summary>
  /// <param name="parameters">
  ///   The parameters.
  /// </param>
  public delegate object InvokeHandler(params object[] parameters);
}