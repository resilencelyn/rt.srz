// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeathMarks.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The death marks.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.enumerations.resolve
{
  /// <summary>
  ///   The death marks.
  /// </summary>
  public static class DeathMarks
  {
    #region Public Methods and Operators

    /// <summary>
    /// The death mark from string.
    /// </summary>
    /// <param name="deathmark">
    /// The deathmark.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    public static bool DeathMarkFromString(string deathmark)
    {
      string str;
      return ((str = deathmark) != null) && (str == "Y");
    }

    #endregion
  }
}