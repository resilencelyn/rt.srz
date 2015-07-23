// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Box.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Короб
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.card.boxes
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   Короб
  /// </summary>
  [Serializable]
  [XmlRoot("BOX")]
  public class Box
  {
    #region Public Properties

    /// <summary>
    ///   СМО
    /// </summary>
    [XmlElement(ElementName = "SMO")]
    public CompanyId CompanyId { get; set; }

    /// <summary>
    ///   Изготовленые полиса
    /// </summary>
    [XmlElement(ElementName = "MDP")]
    public List<MadePolicy> MadePolicies { get; set; }

    /// <summary>
    ///   Номер бокса
    /// </summary>
    [XmlElement(ElementName = "ID")]
    public string Number { get; set; }

    /// <summary>
    ///   Пункт выдачи полисов
    /// </summary>
    [XmlElement(ElementName = "PKT")]
    public EiStructure PolicyIssuingPointId { get; set; }

    #endregion
  }
}