// --------------------------------------------------------------------------------------------------------------------
// <copyright file="REC.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rec type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The rec type.
  /// </summary>
  [Serializable]
  [XmlRoot("REC", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
  [DataContract]
  public class RECType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the addres g.
    /// </summary>
    [XmlElement("ADDRES_G", Order = 11)]
    [DataMember(Name = "ADDRES_G", Order = 11)]
    public AddressType AddresG { get; set; }

    /// <summary>
    ///   Gets or sets the addres p.
    /// </summary>
    [XmlElement("ADDRES_P", Order = 12)]
    [DataMember(Name = "ADDRES_P", Order = 12)]
    public AddressType AddresP { get; set; }

    /// <summary>
    ///   Gets or sets the attachment.
    /// </summary>
    [XmlElement("ATTACHMENT", Order = 16)]
    [DataMember(Name = "ATTACHMENT", Order = 16)]
    public List<AttachmentType> Attachment { get; set; }

    /// <summary>
    ///   Gets or sets the doc.
    /// </summary>
    [XmlElement("DOC_LIST", Order = 10)]
    [DataMember(Name = "DOC_LIST", Order = 10)]
    public List<DocType> Doc { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement("ID", Order = 0)]
    [DataMember(Name = "ID", Order = 0)]
    public Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets the insurance.
    /// </summary>
    [XmlElement("INSURANCE", Order = 14)]
    [DataMember(Name = "INSURANCE", Order = 14)]
    public InsuranceType Insurance { get; set; }

    /// <summary>
    ///   Gets or sets the is active.
    /// </summary>
    [XmlElement("IS_ACTIVE", Order = 2)]
    [DataMember(Name = "IS_ACTIVE", Order = 2)]
    public string IsActive { get; set; }

    /// <summary>
    ///   Gets or sets the statement change.
    /// </summary>
    [XmlElement("NEED_NEW_POLICY", Order = 18)]
    [DataMember(Name = "NEED_NEW_POLICY", Order = 18)]
    public bool NeedNewPolicy { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement("PERSON", Order = 9)]
    [DataMember(Name = "PERSON", Order = 9)]
    public PersonType Person { get; set; }

    /// <summary>
    ///   Gets or sets the person b.
    /// </summary>
    [XmlElement("PERSONB", Order = 15)]
    [DataMember(Name = "PERSONB", Order = 15)]
    public List<PersonBType> PersonB { get; set; }

    /// <summary>
    ///   Gets or sets the statement change.
    /// </summary>
    [XmlElement("CHANGES", Order = 17)]
    [DataMember(Name = "CHANGES", Order = 17)]
    public List<StatementChange> StatementChange { get; set; }

    /// <summary>
    ///   Gets or sets the state begin date.
    /// </summary>
    [XmlElement("VERSION", Order = 4)]
    [DataMember(Name = "VERSION", Order = 4)]
    public string Version { get; set; }

    /// <summary>
    ///   Gets or sets the vizit.
    /// </summary>
    [XmlElement("VIZIT", Order = 13)]
    [DataMember(Name = "VIZIT", Order = 13)]
    public VizitType Vizit { get; set; }

    #endregion
  }
}