// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatisticInitialLoading.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The statistic initial loading.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.dto
{
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.atl.model.atl;

  /// <summary>
  /// The statistic initial loading.
  /// </summary>
  public class StatisticInitialLoading : person
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the count.
    /// </summary>
    [XmlElement]
    [DataMember]
    public virtual int Count { get; set; }

    #endregion
  }
}