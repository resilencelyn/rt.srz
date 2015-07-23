// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchAlgorithm.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search algorithm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations
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