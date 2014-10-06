// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CneZwpStructure.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cne zwp structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The cne zwp structure.
  /// </summary>
  [Serializable]
  public class CneZwpStructure
  {
    #region Fields

    /// <summary>
    ///   The cne 1.
    /// </summary>
    [XmlElement(ElementName = "CNE.1", Order = 1)]
    public string Cne1;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CNE.3", Order = 3)]
    public string Oid;

    #endregion
  }
}