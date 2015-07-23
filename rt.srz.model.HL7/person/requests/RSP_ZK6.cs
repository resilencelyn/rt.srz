// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK6.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The rs p_ z k 6.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK6", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK6 : BaseAnswerMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The qak.
    /// </summary>
    [XmlElement(ElementName = "QAK", Order = 6)]
    public QAK Qak = new QAK();

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the qpd.
    /// </summary>
    [XmlElement(ElementName = "QPD", Order = 4)]
    public Qpd Qpd { get; set; }

    /// <summary>
    ///   Gets or sets the query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK6.QUERY_RESPONSE", Order = 5)]
    public List<QueryResponse_ZK6> QueryResponseList { get; set; }

    #endregion
  }
}