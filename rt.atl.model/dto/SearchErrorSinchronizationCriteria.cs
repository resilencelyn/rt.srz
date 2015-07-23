// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchErrorSinchronizationCriteria.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search error sinchronization criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.atl.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;

  /// <summary>
  /// The search error sinchronization criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchErrorSinchronizationCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    ///   Дата c
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    ///   Дата по
    /// </summary>
    [XmlElement]
    [DataMember]
    public DateTime? DateTo { get; set; }

    #endregion
  }
}