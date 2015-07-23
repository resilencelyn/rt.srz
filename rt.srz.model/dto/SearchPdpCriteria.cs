// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchPdpCriteria.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search pdp criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;

  /// <summary>
  /// The search pdp criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchPdpCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the short name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string ShortName { get; set; }

    #endregion
  }
}