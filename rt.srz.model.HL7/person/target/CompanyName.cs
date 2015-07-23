// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyName.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The company name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The company name.
  /// </summary>
  [Serializable]
  public class CompanyName
  {
    #region Fields

    /// <summary>
    ///   The description type.
    /// </summary>
    [XmlElement(ElementName = "XON.2", Order = 2)]
    public string DescriptionType;

    /// <summary>
    ///   The identificator type.
    /// </summary>
    [XmlElement(ElementName = "XON.7", Order = 7)]
    public string IdentificatorType;

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "XON.1", Order = 1)]
    public string Name;

    /// <summary>
    ///   The organization id.
    /// </summary>
    [XmlElement(ElementName = "XON.10", Order = 10)]
    public string OrganizationId;

    #endregion
  }
}