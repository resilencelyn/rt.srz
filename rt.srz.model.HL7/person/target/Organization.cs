// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Organization.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The organization.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The organization.
  /// </summary>
  [Serializable]
  public class Organization
  {
    #region Fields

    /// <summary>
    ///   The description.
    /// </summary>
    [XmlElement(ElementName = "XON.1", Order = 1)]
    public string Description;

    /// <summary>
    ///   The description type.
    /// </summary>
    [XmlElement(ElementName = "XON.2", Order = 2)]
    public string DescriptionType = "DN";

    /// <summary>
    ///   The identificator type.
    /// </summary>
    [XmlElement(ElementName = "XON.7", Order = 7)]
    public string IdentificatorType;

    /// <summary>
    ///   The organization id.
    /// </summary>
    [XmlElement(ElementName = "XON.10", Order = 10)]
    public string OrganizationId;

    #endregion
  }
}