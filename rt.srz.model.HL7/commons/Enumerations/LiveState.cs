// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LiveState.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The live state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Enumerations
{
  /// <summary>
  ///   The live state.
  /// </summary>
  public enum LiveState
  {
    /// <summary>
    ///   The normal.
    /// </summary>
    Normal, 

    /// <summary>
    ///   The fatal.
    /// </summary>
    Fatal, 

    /// <summary>
    ///   The exit expected.
    /// </summary>
    ExitExpected, 

    /// <summary>
    ///   The restart expected.
    /// </summary>
    RestartExpected, 

    /// <summary>
    ///   The fatal restart expected.
    /// </summary>
    FatalRestartExpected
  }
}