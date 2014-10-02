// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortDirection.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The sort direction.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace rt.core.model.dto.enumerations
{
// Summary:
  // Specifies the direction in which to sort a list of items.
  /// <summary>
  ///   The sort direction.
  /// </summary>
  public enum SortDirection
  {
    /// <summary>
    ///   The default.
    /// </summary>
    Default = 0, 

    // Summary:
    // Sort from smallest to largest. For example, from A to Z.
    /// <summary>
    ///   The ascending.
    /// </summary>
    Ascending = 1, 


// Summary:
    // Sort from largest to smallest. For example, from Z to A.
    /// <summary>
    ///   The descending.
    /// </summary>
    Descending = 2, 
  }
}