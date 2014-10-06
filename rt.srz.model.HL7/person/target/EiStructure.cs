// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EiStructure.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ei structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The ei structure.
  /// </summary>
  [Serializable]
  public class EiStructure
  {
    #region Fields

    /// <summary>
    ///   The identificator.
    /// </summary>
    [XmlElement(ElementName = "EI.1", Order = 1)]
    public string Identificator;

    /// <summary>
    ///   The iso.
    /// </summary>
    [XmlElement(ElementName = "EI.4", Order = 4)]
    public string Iso = "ISO";

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "EI.3", Order = 3)]
    public string Oid;

    /// <summary>
    ///   The organization code.
    /// </summary>
    [XmlElement(ElementName = "EI.2", Order = 2)]
    public string OrganizationCode;

    #endregion
  }
}