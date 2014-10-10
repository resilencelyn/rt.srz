// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Country.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The country.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The country.
  /// </summary>
  [Serializable]
  public class Country
  {
    #region Fields

    /// <summary>
    ///   The code.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string Code;

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "HD.2", Order = 2)]
    public string Name;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "HD.3", Order = 3)]
    public string Oid;

    #endregion
  }
}