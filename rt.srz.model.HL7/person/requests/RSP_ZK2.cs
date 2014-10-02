﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 2.
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
  ///   The rs p_ z k 2.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK2", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK2 : BaseAnswerMessageTemplate, IRspZk
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK2.QUERY_RESPONSE")]
    public List<QueryResponse> QueryResponseList { get; set; }

    #endregion
  }
}