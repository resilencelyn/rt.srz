// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBarcodeEventHandler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The string barcode event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;

#endregion

namespace rt.srz.barcode.reader.delegates
{
  /// <summary>
  ///   The string barcode event handler.
  /// </summary>
  /// <param name="args"> The args. </param>
  [ComVisible(false)]
  public delegate void StringBarcodeEventHandler(string args);
}