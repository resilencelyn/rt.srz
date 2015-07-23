// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PidCard.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The pid card.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The pid card.
  /// </summary>
  [Serializable]
  public class PidCard
  {
    #region Fields

    /// <summary>
    ///   The address list.
    /// </summary>
    [XmlElement(ElementName = "PID.11", Order = 11)]
    public List<AddressCard> AddressList = new List<AddressCard>();

    /// <summary>
    ///   The another identity.
    /// </summary>
    [XmlElement(ElementName = "PID.32", Order = 32)]
    public List<string> AnotherIdentity = new List<string>();

    /// <summary>
    ///   The birth day.
    /// </summary>
    [XmlElement(ElementName = "PID.7", Order = 7)]
    public string BirthDay;

    /// <summary>
    ///   The fio list.
    /// </summary>
    [XmlElement(ElementName = "PID.5", Order = 5)]
    public List<Fio> FioList = new List<Fio>();

    /// <summary>
    ///   The identificators list.
    /// </summary>
    [XmlElement(ElementName = "PID.3", Order = 3)]
    public List<IdentificatorsCard> IdentificatorsList = new List<IdentificatorsCard>();

    /// <summary>
    ///   The nationality.
    /// </summary>
    [XmlElement(ElementName = "PID.26", Order = 26)]
    public National Nationality = new National();

    /// <summary>
    ///   The place of birth.
    /// </summary>
    [XmlElement(ElementName = "PID.23", Order = 23)]
    public string PlaceOfBirth;

    /// <summary>
    ///   The sex.
    /// </summary>
    [XmlElement(ElementName = "PID.8", Order = 8)]
    public string Sex;

    /// <summary>
    ///   The telecommunication addresse list.
    /// </summary>
    [XmlElement(ElementName = "PID.13", Order = 13)]
    public List<TelecommunicationAddress> TelecommunicationAddresseList = new List<TelecommunicationAddress>();

    #endregion
  }
}