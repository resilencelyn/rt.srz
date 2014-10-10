// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZPI_ZA7.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zp i_ z a 7.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.messages
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  // TODO Namespace = "urn:Hl7-org:v2xml"
  #endregion

  /// <summary>
  ///   The zp i_ z a 7.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ZPI_ZA7")]
  public class ZPI_ZA7 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The in 1.
    /// </summary>
    [XmlElement(ElementName = "IN1", Order = 3)]
    public In1Card In1 = new In1Card();

    /// <summary>
    ///   The nk 1.
    /// </summary>
    [XmlElement(ElementName = "NK1", Order = 4)]
    public Nk1 Nk1 = new Nk1();

    /// <summary>
    ///   The zah.
    /// </summary>
    [XmlElement(ElementName = "ZAH", Order = 2)]
    public Zah Zah = new Zah();

    /// <summary>
    ///   The znd.
    /// </summary>
    [XmlElement(ElementName = "ZND", Order = 5)]
    public List<Znd> Znd = new List<Znd>();

    #endregion
  }
}