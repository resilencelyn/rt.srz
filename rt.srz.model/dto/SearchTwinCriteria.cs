// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchTwinCriteria.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search twin criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto;
  using rt.srz.model.enumerations;

  /// <summary>
  /// The search twin criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class SearchTwinCriteria : BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the key id.
    /// </summary>
    [XmlElement]
    [DataMember]
    public Guid? KeyId { get; set; }

    /// <summary>
    /// Gets or sets the key type.
    /// </summary>
    [XmlElement]
    [DataMember]
    public TwinKeyType KeyType { get; set; }

    #endregion
  }
}