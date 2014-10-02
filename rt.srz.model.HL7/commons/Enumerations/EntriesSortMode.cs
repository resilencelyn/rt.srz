// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntriesSortMode.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The entries sort mode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Enumerations
{
  /// <summary>
  ///   The entries sort mode.
  /// </summary>
  public enum EntriesSortMode
  {
    /// <summary>
    ///   The unsorted.
    /// </summary>
    Unsorted, 

    /// <summary>
    ///   The name ascending.
    /// </summary>
    NameAscending, 

    /// <summary>
    ///   The name descending.
    /// </summary>
    NameDescending, 

    /// <summary>
    ///   The creation point ascending.
    /// </summary>
    CreationPointAscending, 

    /// <summary>
    ///   The creation point descending.
    /// </summary>
    CreationPointDescending, 

    /// <summary>
    ///   The modification point ascending.
    /// </summary>
    ModificationPointAscending, 

    /// <summary>
    ///   The modification point descending.
    /// </summary>
    ModificationPointDescending
  }
}