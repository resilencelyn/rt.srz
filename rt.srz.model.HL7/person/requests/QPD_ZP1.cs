// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QPD_ZP1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qp d_ z p 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The qp d_ z p 1.
  /// </summary>
  [Serializable]
  public class QPD_ZP1
  {
    #region Fields

    /// <summary>
    ///   The algorithm compare.
    /// </summary>
    [XmlElement(ElementName = "QPD.3", Order = 3)]
    public string AlgorithmCompare = "У";

    /// <summary>
    ///   The birth day.
    /// </summary>
    [XmlElement(ElementName = "QPD.7", Order = 7)]
    public string BirthDay;

    /// <summary>
    ///   The date request.
    /// </summary>
    [XmlElement(ElementName = "QPD.4", Order = 4)]
    public string DateRequest;

    /// <summary>
    ///   The fio list.
    /// </summary>
    [XmlElement(ElementName = "QPD.6", Order = 6)]
    public List<Fio> FioList = new List<Fio>();

    /// <summary>
    ///   The identificators list.
    /// </summary>
    [XmlElement(ElementName = "QPD.5", Order = 5)]
    public List<Identificators> IdentificatorsList = new List<Identificators>();

    /// <summary>
    ///   The insurance ser num.
    /// </summary>
    [XmlElement(ElementName = "QPD.11", Order = 11)]
    public string InsuranceSerNum;

    /// <summary>
    ///   The insurance territory.
    /// </summary>
    [XmlElement(ElementName = "QPD.20", Order = 20)]
    public string InsuranceTerritory;

    /// <summary>
    ///   The insurance type.
    /// </summary>
    [XmlElement(ElementName = "QPD.10", Order = 10)]
    public string InsuranceType;

    /// <summary>
    ///   The message name.
    /// </summary>
    [XmlElement(ElementName = "QPD.1", Order = 1)]
    public MessageName MessageName = new MessageName();

    /// <summary>
    ///   The place of birth.
    /// </summary>
    [XmlElement(ElementName = "QPD.9", Order = 9)]
    public string PlaceOfBirth;

    /// <summary>
    ///   The sex.
    /// </summary>
    [XmlElement(ElementName = "QPD.8", Order = 8)]
    public string Sex;

    #endregion
  }
}