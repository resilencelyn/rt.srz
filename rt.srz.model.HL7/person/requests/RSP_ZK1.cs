// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RSP_ZK1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rs p_ z k 1.
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
  ///   The rs p_ z k 1.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "RSP_ZK1", Namespace = "urn:hl7-org:v2xml")]
  public class RSP_ZK1 : BaseAnswerMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The query response list.
    /// </summary>
    [XmlElement(ElementName = "RSP_ZK1.QUERY_RESPONSE")]
    public List<QueryResponseZP1> QueryResponseList = new List<QueryResponseZP1>();

    #endregion
  }
}