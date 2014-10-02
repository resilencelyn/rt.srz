// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetReaderListResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The get reader list result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Collections.Generic;
  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The get reader list result.
  /// </summary>
  [ComVisible(true)]
  public class GetReaderListResult : OperationResult
  {
    #region Public Properties

    /// <summary>
    ///   Èìÿ
    /// </summary>
    public virtual List<string> ReaderNameList { get; set; }

    #endregion
  }
}