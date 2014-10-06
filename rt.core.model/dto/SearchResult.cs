// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResult.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The search result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.core.model.dto
{
  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  /// <summary>
  /// The search result.
  /// </summary>
  /// <typeparam name="T">
  /// тип строки
  /// </typeparam>
  [Serializable]
  [DataContract]
  public class SearchResult<T>
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the rows.
    /// </summary>
    [XmlElement]
    [DataMember]
    public IList<T> Rows { get; set; }

    /// <summary>
    ///   Gets or sets the skip.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Skip { get; set; }

    /// <summary>
    ///   Gets or sets the total.
    /// </summary>
    [XmlElement]
    [DataMember]
    public int Total { get; set; }

    #endregion
  }
}