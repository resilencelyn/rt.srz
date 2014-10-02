// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zpw.cs" company="Rintech">
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
  [XmlType(TypeName = "ZPW", Namespace = "urn:hl7-org:v2xml")]
  public class Zpw
  {
    #region Public Properties

    /// <summary>
    ///   Отчётный период
    /// </summary>
    [XmlElement("ZPW.1", Order = 1)]
    public Identificators Enp { get; set; }

    #endregion
  }
}