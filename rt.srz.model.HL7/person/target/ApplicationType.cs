// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationType.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The application type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The application type.
  /// </summary>
  [Serializable]
  public class ApplicationType
  {
    #region Fields

    /// <summary>
    ///   The alternative name.
    /// </summary>
    [XmlElement(ElementName = "CWE.5", Order = 5)]
    public string AlternativeName;

    /// <summary>
    ///   The alternative name version.
    /// </summary>
    [XmlElement(ElementName = "CWE.8", Order = 8)]
    public string AlternativeNameVersion;

    /// <summary>
    ///   The another name.
    /// </summary>
    [XmlElement(ElementName = "CWE.9", Order = 9)]
    public string AnotherName;

    /// <summary>
    ///   The main name.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string MainName;

    /// <summary>
    ///   The main name version.
    /// </summary>
    [XmlElement(ElementName = "CWE.7", Order = 7)]
    public string MainNameVersion;

    #endregion
  }
}