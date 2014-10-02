// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorOccurredEventHandler.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The error occurred event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System.IO;

#endregion

namespace rt.srz.barcode.reader.delegates
{
  /// <summary>
  ///   The error occurred event handler.
  /// </summary>
  /// <param name="sender"> The sender. </param>
  /// <param name="e"> The e. </param>
  public delegate void ErrorOccurredEventHandler(object sender, ErrorEventArgs e);
}