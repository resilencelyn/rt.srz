// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryResponseZK6.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The query response_ z k 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The query response_ z k 6.
  /// </summary>
  [Serializable]
  public class QueryResponse_ZK6
  {
    #region Fields

    /// <summary>
    ///   The zwl list.
    /// </summary>
    [XmlElement(ElementName = "ZWL")]
    public List<Zwl> ZWLList = new List<Zwl>();

    #endregion
  }
}