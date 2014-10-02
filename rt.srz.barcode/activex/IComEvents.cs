// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IComEvents.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The ComEvents interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;
using rt.srz.model.barcode;

#endregion

namespace rt.srz.barcode.activex
{
  /// <summary>
  ///   The ComEvents interface.
  /// </summary>
  [Guid("ECA5DD1D-096E-440c-BA6A-0118D351650B")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
  public interface IComEvents
  {
    /// <summary>
    /// The beep event.
    /// </summary>
    /// <param name="args">
    /// The args. 
    /// </param>
    [DispId(0x00000001)]
    void BeepEvent(string args);

    /// <summary>
    /// The beep event.
    /// </summary>
    /// <param name="args">
    /// The args. 
    /// </param>
    [DispId(0x00000002)]
    void DataRecieved(PolicyData args);
  }
}