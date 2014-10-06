// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Address.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The address.
  /// </summary>
  [Serializable]
  public class Address
  {
    #region Fields

    /// <summary>
    ///   The address type.
    /// </summary>
    [XmlElement(ElementName = "XAD.7", Order = 7)]
    public string AddressType;

    /// <summary>
    ///   The country.
    /// </summary>
    [XmlElement(ElementName = "XAD.6", Order = 6)]
    public string Country;

    /// <summary>
    ///   The region.
    /// </summary>
    [XmlElement(ElementName = "XAD.9", Order = 9)]
    public string Region;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Address" /> class.
    /// </summary>
    public Address()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Address"/> class.
    /// </summary>
    /// <param name="addressType">
    /// The address type.
    /// </param>
    /// <param name="region">
    /// The region.
    /// </param>
    public Address(string addressType, string region)
    {
      AddressType = addressType;
      Region = region;
    }

    #endregion
  }
}