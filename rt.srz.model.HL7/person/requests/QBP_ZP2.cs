// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QBP_ZP2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The qb p_ z p 2.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   The qb p_ z p 2.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "QBP_ZP2", Namespace = "urn:hl7-org:v2xml")]
  public class QBP_ZP2 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The qpd.
    /// </summary>
    [XmlElement(ElementName = "QPD", Order = 2)]
    public Qpd Qpd = new Qpd();

    #endregion
  }
}