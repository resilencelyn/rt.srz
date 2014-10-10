// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zpw.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Отчётный период
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Отчётный период
  /// </summary>
  [Serializable]
  [XmlType(TypeName = "ZPW", Namespace = "urn:Hl7-org:v2xml")]
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