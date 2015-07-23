// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchSmoCriteria.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search smo criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;

  /// <summary>
  /// The search smo criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchSmoCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the oid.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Oid { get; set; }

    /// <summary>
    /// Gets or sets the short name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string ShortName { get; set; }

    #endregion
  }
}