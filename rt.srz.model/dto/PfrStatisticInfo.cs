// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PfrStatisticInfo.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The pfr statistic info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  /// <summary>
  /// The pfr statistic info.
  /// </summary>
  public class PfrStatisticInfo
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the employed record count.
    /// </summary>
    public int EmployedRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the found by data record count.
    /// </summary>
    public int FoundByDataRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the found by snils record count.
    /// </summary>
    public int FoundBySnilsRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the insured record count.
    /// </summary>
    public int InsuredRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the not found record count.
    /// </summary>
    public int NotFoundRecordCount { get; set; }

    /// <summary>
    /// Gets or sets the total record count.
    /// </summary>
    public int TotalRecordCount { get; set; }

    #endregion
  }
}