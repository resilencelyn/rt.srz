// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvokeHandler.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
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