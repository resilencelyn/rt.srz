// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchAutoCompleteCriteria.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search auto complete criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;

  /// <summary>
  /// The search auto complete criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchAutoCompleteCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [XmlElement]
    [DataMember]
    public string Name { get; set; }

    #endregion
  }
}