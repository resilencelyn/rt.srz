// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK5.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 5.
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
  ///   The rs p_ z k 5.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK5", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK5 : BaseAnswerMessageTemplate, IRspZk
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK5.QUERY_RESPONSE")]
    public List<QueryResponse> QueryResponseList { get; set; }

    #endregion
  }
}