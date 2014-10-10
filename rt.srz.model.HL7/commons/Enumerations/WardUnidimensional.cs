// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WardUnidimensional.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ward unidimensional.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.commons.Enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The ward unidimensional.
  /// </summary>
  [Flags]
  public enum WardUnidimensional
  {
    /// <summary>
    ///   The none.
    /// </summary>
    None, 

    /// <summary>
    ///   The forward.
    /// </summary>
    Forward, 

    /// <summary>
    ///   The back.
    /// </summary>
    Back, 

    /// <summary>
    ///   The both.
    /// </summary>
    Both
  }
}