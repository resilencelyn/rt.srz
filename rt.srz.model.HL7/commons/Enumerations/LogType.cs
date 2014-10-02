// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The log type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.commons.Enumerations
{
  #region references

  using System;

  #endregion

  /// <summary>
  ///   The log type.
  /// </summary>
  [Flags]
  public enum LogType
  {
    /// <summary>
    ///   The all.
    /// </summary>
    All = 0xffff, 

    /// <summary>
    ///   The console.
    /// </summary>
    Console = 1, 

    /// <summary>
    ///   The file.
    /// </summary>
    File = 0x2000, 

    /// <summary>
    ///   The local.
    /// </summary>
    Local = 0x3003, 

    /// <summary>
    ///   The memo.
    /// </summary>
    Memo = 0x1000, 

    /// <summary>
    ///   The os log.
    /// </summary>
    OsLog = 0x10, 

    /// <summary>
    ///   The trace.
    /// </summary>
    Trace = 2
  }
}