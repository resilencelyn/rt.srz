// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICodeMessageResolver!1.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The CodeMessageResolver interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Interfaces
{
  /// <summary>
  /// The CodeMessageResolver interface.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public interface ICodeMessageResolver<T>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The message by code.
    /// </summary>
    /// <param name="code">
    /// The code.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    string MessageByCode(T code);

    #endregion
  }
}