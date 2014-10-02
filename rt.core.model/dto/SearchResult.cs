// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResult.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The search result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region



#endregion

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
  }
}