// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataRecievedEventArgs.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The data recieved event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using rt.srz.model.barcode;

#endregion

namespace rt.srz.barcode.reader.delegates.args
{
  /// <summary>
  ///   The data recieved event args.
  /// </summary>
  public class DataRecievedEventArgs : EventArgs
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRecievedEventArgs"/> class.
    /// </summary>
    /// <param name="data">
    /// The data. 
    /// </param>
    public DataRecievedEventArgs(string data)
    {
      ReadedStr = data;
      PolicyData = (new PolicyData()).Create(data);
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets the policy data.
    /// </summary>
    public PolicyData PolicyData { get; private set; }

    /// <summary>
    ///   The readed str.
    /// </summary>
    public string ReadedStr { get; private set; }

    #endregion
  }
}