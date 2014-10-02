// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IScanerListener.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The ScanerListener interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;

#endregion

namespace rt.srz.barcode.activex
{
  /// <summary>
  ///   The ScanerListener interface.
  /// </summary>
  [Guid("4B3AE7D8-FB6A-4558-8A96-BF82B54F329C")]
  [ComVisible(true)]
  public interface IScanerListener
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The dispose.
    /// </summary>
    [DispId(0x10000002)]
    void Dispose();

    /// <summary>
    ///   The greeting.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    [DispId(0x10000003)]
    string Ping();

    /// <summary>
    /// The start.
    /// </summary>
    /// <param name="portName">
    /// The port name. 
    /// </param>
    [DispId(0x10000004)]
    void Start(string portName);

    /// <summary>
    /// The test beep.
    /// </summary>
    /// <param name="str">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    [DispId(0x10000001)]
    int TestBeep(string str);

    #endregion
  }
}