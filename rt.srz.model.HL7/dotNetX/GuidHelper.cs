// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GuidHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The guid helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.dotNetX
{
  #region references

  using System;
  using System.Text.RegularExpressions;

  #endregion

  /// <summary>
  ///   The guid helper.
  /// </summary>
  public static class GuidHelper
  {
    #region Static Fields

    /// <summary>
    ///   The guid reg ex.
    /// </summary>
    public static readonly Regex GuidRegEx =
      new Regex(
        @"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");

    #endregion

    #region Public Methods and Operators

    /// <summary>
    /// The is null or empty.
    /// </summary>
    /// <param name="Guid">
    /// The guid.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsNullOrEmpty(Guid Guid)
    {
      return Guid.Equals(Guid.Empty);
    }

    /// <summary>
    /// The is valid guid.
    /// </summary>
    /// <param name="Guid">
    /// The guid.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool IsValidGuid(string Guid)
    {
      return !string.IsNullOrEmpty(Guid) && GuidRegEx.IsMatch(Guid);
    }

    #endregion
  }
}