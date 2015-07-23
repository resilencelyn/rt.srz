// --------------------------------------------------------------------------------------------------------------------
// <copyright file="National.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The national.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The national.
  /// </summary>
  [Serializable]
  public class National
  {
    #region Fields

    /// <summary>
    ///   The country.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string Country;

    /// <summary>
    ///   The description.
    /// </summary>
    [XmlElement(ElementName = "CWE.9", Order = 9)]
    public string Description;

    /// <summary>
    ///   The nationality.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string Nationality;

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string TableCode;

    #endregion
  }
}