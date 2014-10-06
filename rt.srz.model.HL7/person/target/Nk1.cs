// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Nk1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The nk 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The nk 1.
  /// </summary>
  [Serializable]
  public class Nk1
  {
    #region Fields

    /// <summary>
    ///   The address.
    /// </summary>
    [XmlElement(ElementName = "NK1.4", Order = 4)]
    public AddressCard Address = new AddressCard();

    /// <summary>
    ///   The birth day.
    /// </summary>
    [XmlElement(ElementName = "NK1.16", Order = 16)]
    public string BirthDay;

    /// <summary>
    ///   The degree of relationship.
    /// </summary>
    [XmlElement(ElementName = "NK1.3", Order = 3)]
    public Document DegreeOfRelationship = new Document();

    /// <summary>
    ///   The fio.
    /// </summary>
    [XmlElement(ElementName = "NK1.2", Order = 2)]
    public Fio Fio = new Fio();

    /// <summary>
    ///   The id.
    /// </summary>
    [XmlElement(ElementName = "NK1.1", Order = 1)]
    public string Id;

    /// <summary>
    ///   The identificator list.
    /// </summary>
    [XmlElement(ElementName = "NK1.33", Order = 33)]
    public List<IdentificatorsCard> IdentificatorList = new List<IdentificatorsCard>();

    /// <summary>
    ///   The national.
    /// </summary>
    [XmlElement(ElementName = "NK1.19", Order = 19)]
    public National National = new National();

    /// <summary>
    ///   The organization.
    /// </summary>
    [XmlElement(ElementName = "NK1.13", Order = 13)]
    public Organization Organization = new Organization();

    /// <summary>
    ///   The place of birth.
    /// </summary>
    [XmlElement(ElementName = "NK1.38", Order = 38)]
    public string PlaceOfBirth;

    /// <summary>
    ///   The position.
    /// </summary>
    [XmlElement(ElementName = "NK1.10", Order = 10)]
    public string Position;

    /// <summary>
    ///   The role.
    /// </summary>
    [XmlElement(ElementName = "NK1.7", Order = 7)]
    public Role Role = new Role();

    /// <summary>
    ///   The sex.
    /// </summary>
    [XmlElement(ElementName = "NK1.15", Order = 15)]
    public string Sex;

    /// <summary>
    ///   The telecommunication addresse list.
    /// </summary>
    [XmlElement(ElementName = "NK1.5", Order = 5)]
    public List<TelecommunicationAddress> TelecommunicationAddresseList = new List<TelecommunicationAddress>();

    #endregion
  }
}