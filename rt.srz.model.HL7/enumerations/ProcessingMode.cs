// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingMode.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The processing mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The processing mode.
  /// </summary>
  [CLSCompliant(false)]
  public enum ProcessingMode : byte
  {
    /// <summary>
    ///   The debug.
    /// </summary>
    Debug = 20, 

    /// <summary>
    ///   The process.
    /// </summary>
    Process = 10, 

    /// <summary>
    ///   The training.
    /// </summary>
    Training = 30, 

    /// <summary>
    ///   The unknown.
    /// </summary>
    Unknown = 0
  }
}