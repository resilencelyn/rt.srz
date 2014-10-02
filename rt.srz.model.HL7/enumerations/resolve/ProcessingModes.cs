// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessingModes.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The processing modes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.enumerations.resolve
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The processing modes.
  /// </summary>
  [CLSCompliant(false)]
  public static class ProcessingModes
  {
    #region Public Methods and Operators

    /// <summary>
    /// The processing mode from string.
    /// </summary>
    /// <param name="mode">
    /// The mode.
    /// </param>
    /// <returns>
    /// The <see cref="ProcessingMode"/>.
    /// </returns>
    public static ProcessingMode ProcessingModeFromString(string mode)
    {
      switch (mode)
      {
        case "P":
          return ProcessingMode.Process;

        case "D":
          return ProcessingMode.Debug;

        case "T":
          return ProcessingMode.Training;
      }

      return ProcessingMode.Unknown;
    }

    /// <summary>
    /// The processing mode to string.
    /// </summary>
    /// <param name="mode">
    /// The mode.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ProcessingModeToString(ProcessingMode mode)
    {
      var mode2 = mode;
      if (mode2 != ProcessingMode.Process)
      {
        if (mode2 == ProcessingMode.Debug)
        {
          return "D";
        }

        if (mode2 == ProcessingMode.Training)
        {
          return "T";
        }

        return string.Empty;
      }

      return "P";
    }

    #endregion
  }
}