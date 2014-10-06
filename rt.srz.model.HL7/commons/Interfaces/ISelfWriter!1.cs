// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISelfWriter!1.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The SelfWriter interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Interfaces
{
  /// <summary>
  /// The SelfWriter interface.
  /// </summary>
  /// <typeparam name="T">
  /// </typeparam>
  public interface ISelfWriter<T>
  {
    #region Public Methods and Operators

    /// <summary>
    /// The write to.
    /// </summary>
    /// <param name="target">
    /// The target.
    /// </param>
    void WriteTo(T target);

    #endregion
  }
}