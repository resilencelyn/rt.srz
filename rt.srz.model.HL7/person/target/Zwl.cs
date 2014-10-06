// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zwl.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Перечень ЕНП работающих застрахованных лиц, идентифицированных в РС ЕРЗ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Перечень ЕНП работающих застрахованных лиц, идентифицированных в РС ЕРЗ
  /// </summary>
  [Serializable]
  [XmlType(TypeName = "ZWL", Namespace = "urn:hl7-org:v2xml")]
  public class Zwl
  {
    #region Public Properties

    /// <summary>
    ///   ЕНП застрахованного лица
    /// </summary>
    [XmlElement("ZWL.1", Order = 1)]
    public string Zwl1 { get; set; }

    #endregion
  }
}