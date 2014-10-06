// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinApiHelper.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The win api helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.dotNetX
{
  #region references

  using System;
  using System.Diagnostics;
  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  ///   The win api helper.
  /// </summary>
  [CLSCompliant(false)]
  public static class WinApiHelper
  {
    #region Public Methods and Operators

    /// <summary>
    /// The terminate program.
    /// </summary>
    /// <param name="uExitCode">
    /// The u exit code.
    /// </param>
    public static void TerminateProgram(uint uExitCode = 0xffffffff)
    {
      try
      {
        Unmanaged.ExitProcess(uExitCode);
      }
      catch
      {
        var maxValue = uint.MaxValue;
        Unmanaged.TerminateProcess(Process.GetCurrentProcess().Handle, maxValue);
      }
    }

    #endregion

    /// <summary>
    ///   The unmanaged.
    /// </summary>
    public static class Unmanaged
    {
      #region Public Methods and Operators

      /// <summary>
      /// The exit process.
      /// </summary>
      /// <param name="uExitCode">
      /// The u exit code.
      /// </param>
      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern void ExitProcess(uint uExitCode);

      /// <summary>
      /// The terminate process.
      /// </summary>
      /// <param name="hProcess">
      /// The h process.
      /// </param>
      /// <param name="uExitCode">
      /// The u exit code.
      /// </param>
      /// <returns>
      /// The <see cref="bool"/>.
      /// </returns>
      [return: MarshalAs(UnmanagedType.Bool)]
      [DllImport("kernel32.dll", SetLastError = true)]
      public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

      #endregion
    }
  }
}