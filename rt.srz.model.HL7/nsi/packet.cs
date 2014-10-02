// --------------------------------------------------------------------------------------------------------------------
// <copyright file="packet.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The packet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  [XmlRoot(ElementName = "packet", Namespace = "", IsNullable = false)]
  public class Packet
  {
    #region Public Properties

    /// <summary>
    ///   The date.
    /// </summary>
    [XmlAttribute]
    public string Date { get; set; }

    /// <summary>
    ///   The ins company.
    /// </summary>
    [XmlElement("insCompany", Form = XmlSchemaForm.Unqualified)]
    public List<InsCompany> InsCompany { get; set; }

    /// <summary>
    ///   The med company.
    /// </summary>
    [XmlElement("medCompany", Form = XmlSchemaForm.Unqualified)]
    public List<MedCompany> MedCompany { get; set; }

    /// <summary>
    ///   The tfoms.
    /// </summary>
    [XmlElement("TFOMS", Form = XmlSchemaForm.Unqualified)]
    public List<Tfoms> Tfoms { get; set; }

    /// <summary>
    ///   The version.
    /// </summary>
    [XmlAttribute]
    public string Version { get; set; }

    #endregion
  }
}