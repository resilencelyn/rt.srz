// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QBP_ZP4.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qb p_ z p 4.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The qb p_ z p 4.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "QBP_ZP4", Namespace = "urn:hl7-org:v2xml")]
  public class QBP_ZP4 : BaseMessageTemplate
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