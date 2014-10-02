// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanerListener.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The scaner listener.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Runtime.InteropServices;
using rt.srz.barcode.reader;

#endregion

namespace rt.srz.barcode.activex
{
  /// <summary>
  ///   The scaner listener.
  /// </summary>
  [Guid("3B77EEC1-744E-4190-9CBC-CA5DC5E21461")]
  [ComVisible(true)]
  [ClassInterface(ClassInterfaceType.None)]
  [ComSourceInterfaces(typeof(IComEvents))]
  public class ScanerListener : PortReader, IScanerListener
  {
    #region Public Methods and Operators

    /// <summary>
    ///   The dispose.
    /// </summary>
    public void Dispose()
    {
      Stop();
    }

    /// <summary>
    ///   The greeting.
    /// </summary>
    /// <returns> The <see cref="string" /> . </returns>
    public string Ping()
    {
      return "Hello, World!";
    }

    /// <summary>
    /// The start.
    /// </summary>
    /// <param name="portName">
    /// The port name. 
    /// </param>
    public void Start(string portName)
    {
      if (string.IsNullOrEmpty(portName) || portName == "Не использовать")
      {
        return;
      }

      PortName = portName;
      Start();
    }

    /// <summary>
    /// The test beep.
    /// </summary>
    /// <param name="args">
    /// The str. 
    /// </param>
    /// <returns>
    /// The <see cref="int"/> . 
    /// </returns>
    public int TestBeep(string args)
    {
      OnBeep(args);
      return (int)DateTime.Now.Ticks;
    }

    #endregion
  }
}