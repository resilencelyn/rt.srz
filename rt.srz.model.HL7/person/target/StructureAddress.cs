// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureAddress.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The structure address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The structure address.
  /// </summary>
  [Serializable]
  public class StructureAddress
  {
    #region Fields

    /// <summary>
    ///   The building.
    /// </summary>
    [XmlElement(ElementName = "SAD.3", Order = 3)]
    public string Building;

    /// <summary>
    ///   The room.
    /// </summary>
    [XmlElement(ElementName = "SAD.1", Order = 1)]
    public string Room;

    /// <summary>
    ///   The street.
    /// </summary>
    [XmlElement(ElementName = "SAD.2", Order = 2)]
    public string Street;

    #endregion
  }
}