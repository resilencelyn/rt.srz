// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchAlgorithms.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search algorithms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations.resolve
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The search algorithms.
  /// </summary>
  [CLSCompliant(false)]
  public static class SearchAlgorithms
  {
    #region Public Methods and Operators

    /// <summary>
    /// The algorithm from string.
    /// </summary>
    /// <param name="algorithm">
    /// The algorithm.
    /// </param>
    /// <returns>
    /// The <see cref="SearchAlgorithm"/>.
    /// </returns>
    public static SearchAlgorithm AlgorithmFromString(string algorithm)
    {
      string str;
      if (((str = algorithm) != null) && (str == "У"))
      {
        return SearchAlgorithm.Retargeting;
      }

      return SearchAlgorithm.Unknown;
    }

    /// <summary>
    /// The algorithm to string.
    /// </summary>
    /// <param name="algorithm">
    /// The algorithm.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string AlgorithmToString(SearchAlgorithm algorithm)
    {
      if (algorithm == SearchAlgorithm.Retargeting)
      {
        return "У";
      }

      return null;
    }

    #endregion
  }
}