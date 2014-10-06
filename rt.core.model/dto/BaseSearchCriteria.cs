// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseSearchCriteria.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The base search criteria.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.dto
{
  using System;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  using rt.core.model.dto.enumerations;

  /// <summary>
  /// The base search criteria.
  /// </summary>
  [Serializable]
  [DataContract]
  public class BaseSearchCriteria
  {
    #region Public Properties

    /// <summary>
    /// Gets or sets the current page index.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int CurrentPageIndex { get; set; }

    /// <summary>
    ///   Gets or sets the skip.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Skip { get; set; }

    /// <summary>
    ///   Gets or sets the sort direction.
    /// </summary>
    [XmlElement]
    [DataMember]
    public SortDirection SortDirection { get; set; }

    /// <summary>
    ///   Поле для сортировки
    /// </summary>
    [XmlElement]
    [DataMember]
    public string SortExpression { get; set; }

    /// <summary>
    ///   Gets or sets the take.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Take { get; set; }

    #endregion
  }
}