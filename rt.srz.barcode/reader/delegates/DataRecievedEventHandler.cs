// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRecievedEventHandler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The data recieved event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.Runtime.InteropServices;
using rt.srz.model.barcode;

#endregion

namespace rt.srz.barcode.reader.delegates
{
  /// <summary>
  ///   The data recieved event handler.
  /// </summary>
  /// <param name="e"> The e. </param>
  [ComVisible(false)]
  public delegate void DataRecievedEventHandler(PolicyData e);
}