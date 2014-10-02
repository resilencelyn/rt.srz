// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchAlgorithm.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The search algorithm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The search algorithm.
  /// </summary>
  [CLSCompliant(false)]
  public enum SearchAlgorithm : byte
  {
    /// <summary>
    ///   The retargeting.
    /// </summary>
    Retargeting = 11, 

    /// <summary>
    ///   The unknown.
    /// </summary>
    Unknown = 0
  }
}