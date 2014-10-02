// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK4.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 4.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The rs p_ z k 4.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK4", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK4 : BaseAnswerMessageTemplate, IRspZk
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK4.QUERY_RESPONSE")]
    public List<QueryResponse> QueryResponseList { get; set; }

    #endregion
  }
}