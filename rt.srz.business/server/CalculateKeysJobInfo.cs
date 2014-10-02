// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CalculateJobInfo.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.business.server
{
  #region
  using System;
  using rt.core.business.server.jobpool;
  using rt.srz.model.srz;
  #endregion

  /// <summary>
  ///   The calculate search key task.
  /// </summary>
  [Serializable]
  public class CalculateKeysJobInfo : JobInfo
  {
    #region Public Properties

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculateKeysJobInfo"/> class.
    /// </summary>
    public CalculateKeysJobInfo()
    {
      IsError = false;
    }

    /// <summary>
    ///   Gets or sets the from.
    /// </summary>
    public int From { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is standard.
    /// </summary>
    public bool IsStandard { get; set; }

    /// <summary>
    ///   Gets or sets the search key type.
    /// </summary>
    public SearchKeyType SearchKeyType { get; set; }

    /// <summary>
    ///   Gets or sets the to.
    /// </summary>
    public int To { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is error.
    /// </summary>
    public bool IsError { get; set; }

    #endregion
  }
}