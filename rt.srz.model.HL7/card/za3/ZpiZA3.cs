// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZpiZA3.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ZPI_ZA3
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za3
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.card.target;
  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   ZPI_ZA3
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ZPI_ZA3", Namespace = "urn:hl7-org:v2xml")]
  public class ZpiZA3
  {
    #region Public Properties

    /// <summary>
    ///   INVOICE
    /// </summary>
    [XmlElement("ZPI_ZA3.INVOICE")]
    public List<INVOICE> INVOICE { get; set; }

    /// <summary>
    ///   MSH
    /// </summary>
    public MSH MSH { get; set; }

    /// <summary>
    ///   ZSG
    /// </summary>
    public ZSG ZSG { get; set; }

    #endregion
  }
}