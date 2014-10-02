// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOmsHistoryResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The read oms history result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.uec.service.dto
{
  #region references

  using System.Collections.Generic;
  using System.Runtime.InteropServices;

  #endregion

  /// <summary>
  /// The read oms history result.
  /// </summary>
  [ComVisible(true)]
  public class ReadOMSHistoryResult : OperationResult
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the oms data list.
    /// </summary>
    public List<OMSDataResult> OMSDataList { get; set; }

    #endregion
  }
}