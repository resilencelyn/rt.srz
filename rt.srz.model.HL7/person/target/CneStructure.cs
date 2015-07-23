// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CneStructure.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The cne structure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The cne structure.
  /// </summary>
  [Serializable]
  public class CneStructure
  {
    #region Fields

    /// <summary>
    ///   The code tfoms.
    /// </summary>
    [XmlElement(ElementName = "CNE.4", Order = 4)]
    public string CodeTfoms;

    /// <summary>
    ///   The five digit code.
    /// </summary>
    [XmlElement(ElementName = "CNE.1", Order = 1)]
    public string FiveDigitCode;

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "CNE.2", Order = 2)]
    public string Name;

    /// <summary>
    ///   The name tfoms.
    /// </summary>
    [XmlElement(ElementName = "CNE.5", Order = 5)]
    public string NameTfoms;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CNE.3", Order = 3)]
    public string Oid;

    /// <summary>
    ///   The oid tfoms.
    /// </summary>
    [XmlElement(ElementName = "CNE.6", Order = 6)]
    public string OidTfoms;

    #endregion
  }
}