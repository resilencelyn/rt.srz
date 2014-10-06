// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QBP_ZP1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qb p_ z p 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qb p_ z p 1.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "QBP_ZP1", Namespace = "urn:hl7-org:v2xml")]
  public class QBP_ZP1 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The qpd.
    /// </summary>
    [XmlElement(ElementName = "QPD", Order = 2)]
    public QPD_ZP1 Qpd = new QPD_ZP1();

    #endregion
  }
}