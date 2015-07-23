// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK9.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 9.
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
  ///   The rs p_ z k 9.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK9", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK9 : BaseAnswerMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The qak.
    /// </summary>
    [XmlElement(ElementName = "QAK", Order = 5)]
    public QAK Qak = new QAK();

    /// <summary>
    ///   The query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK9.QUERY_RESPONSE", Order = 4)]
    public List<QueryResponse_ZK9> QueryResponseList = new List<QueryResponse_ZK9>();

    #endregion
  }
}