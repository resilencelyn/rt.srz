// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zwp.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Отчётный период
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Отчётный период
  /// </summary>
  [Serializable]
  [XmlType(TypeName = "ZWP", Namespace = "urn:hl7-org:v2xml")]
  public class Zwp
  {
    #region Public Properties

    /// <summary>
    ///   Отчётный период
    /// </summary>
    [XmlElement("ZWP.1", Order = 1)]
    public CneZwpStructure Zwp1 { get; set; }

    /// <summary>
    ///   Год отчётного периода
    /// </summary>
    [XmlElement("ZWP.2", Order = 2)]
    public string Zwp2 { get; set; }

    /// <summary>
    ///   Территория работы
    /// </summary>
    [XmlElement("ZWP.3", Order = 3)]
    public CneZwpStructure Zwp3 { get; set; }

    #endregion
  }
}