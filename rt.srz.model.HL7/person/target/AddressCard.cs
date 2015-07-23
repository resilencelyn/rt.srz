// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddressCard.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The address card.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Text;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The address card.
  /// </summary>
  [Serializable]
  public class AddressCard
  {
    #region Fields

    /// <summary>
    ///   The address type.
    /// </summary>
    [XmlElement(ElementName = "XAD.7", Order = 7)]
    public string AddressType;

    /// <summary>
    ///   The building.
    /// </summary>
    [XmlElement(ElementName = "XAD.2", Order = 2)]
    public string Building;

    /// <summary>
    ///   The city.
    /// </summary>
    [XmlElement(ElementName = "XAD.3", Order = 3)]
    public string City;

    /// <summary>
    ///   The country code.
    /// </summary>
    [XmlElement(ElementName = "XAD.6", Order = 6)]
    public string CountryCode;

    /// <summary>
    ///   The district.
    /// </summary>
    [XmlElement(ElementName = "XAD.8", Order = 8)]
    public string District;

    /// <summary>
    ///   The is homeless.
    /// </summary>
    [XmlElement(ElementName = "XAD.21", Order = 21)]
    public string IsHomeless;

    /// <summary>
    ///   The postcode.
    /// </summary>
    [XmlElement(ElementName = "XAD.40", Order = 40)]
    public string Postcode;

    /// <summary>
    ///   The region.
    /// </summary>
    [XmlElement(ElementName = "XAD.9", Order = 9)]
    public string Region;

    /// <summary>
    ///   The region name.
    /// </summary>
    [XmlElement(ElementName = "XAD.19", Order = 19)]
    public string RegionName;

    /// <summary>
    ///   The registration date.
    /// </summary>
    [XmlElement(ElementName = "XAD.13", Order = 13)]
    public string RegistrationDate;

    /// <summary>
    ///   The structure address.
    /// </summary>
    [XmlElement(ElementName = "XAD.1", Order = 1)]
    public StructureAddress StructureAddress = new StructureAddress();

    /// <summary>
    ///   The town.
    /// </summary>
    [XmlElement(ElementName = "XAD.20", Order = 20)]
    public string Town;

    /// <summary>
    ///   The use type.
    /// </summary>
    [XmlElement(ElementName = "XAD.18", Order = 18)]
    public string UseType;

    /// <summary>
    ///   The zip.
    /// </summary>
    [XmlElement(ElementName = "XAD.5", Order = 5)]
    public string Zip;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the address str.
    /// </summary>
    [XmlIgnore]
    public string AddressStr
    {
      get
      {
        var sb = new StringBuilder();
        sb.Append(Postcode).Append(" "). // Append(Subject).Append(" ").
          // Append(Area).Append(" ").
           Append(City).Append(" ").Append(Town).Append(" ");
        if (StructureAddress != null)
        {
          sb.Append(StructureAddress.Street)
            .Append(" ")
            .Append(StructureAddress.Building)
            .Append(" ")
            .Append(StructureAddress.Room);
        }

        return sb.ToString();
      }
    }

    #endregion
  }
}