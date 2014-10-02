// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentificatorsCard.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The identificators card.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The identificators card.
  /// </summary>
  [Serializable]
  public class IdentificatorsCard : Identificators
  {
    #region Fields

    /// <summary>
    ///   The actual from.
    /// </summary>
    [XmlElement(ElementName = "CX.7", Order = 7)]
    public string ActualFrom;

    /// <summary>
    ///   The actual to.
    /// </summary>
    [XmlElement(ElementName = "CX.8", Order = 8)]
    public string ActualTo;

    /// <summary>
    ///   The country.
    /// </summary>
    [XmlElement(ElementName = "CX.9", Order = 9)]
    public Country Country;

    /// <summary>
    ///   The organization.
    /// </summary>
    [XmlElement(ElementName = "CX.6", Order = 6)]
    public OrganizationName Organization;

    #endregion
  }
}