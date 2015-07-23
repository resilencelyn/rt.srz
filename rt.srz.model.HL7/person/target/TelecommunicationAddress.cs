// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TelecommunicationAddress.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The telecommunication address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The telecommunication address.
  /// </summary>
  [Serializable]
  public class TelecommunicationAddress
  {
    #region Fields

    /// <summary>
    ///   The city code.
    /// </summary>
    [XmlElement(ElementName = "XTN.6", Order = 6)]
    public string CityCode;

    /// <summary>
    ///   The code.
    /// </summary>
    [XmlElement(ElementName = "XTN.2", Order = 2)]
    public string Code;

    /// <summary>
    ///   The comment.
    /// </summary>
    [XmlElement(ElementName = "XTN.9", Order = 9)]
    public string Comment;

    /// <summary>
    ///   The country code.
    /// </summary>
    [XmlElement(ElementName = "XTN.5", Order = 5)]
    public string CountryCode;

    /// <summary>
    ///   The email.
    /// </summary>
    [XmlElement(ElementName = "XTN.4", Order = 4)]
    public string Email;

    /// <summary>
    ///   The phone.
    /// </summary>
    [XmlElement(ElementName = "XTN.7", Order = 7)]
    public string Phone;

    /// <summary>
    ///   The second phone.
    /// </summary>
    [XmlElement(ElementName = "XTN.8", Order = 8)]
    public string SecondPhone;

    /// <summary>
    ///   The simple phone.
    /// </summary>
    [XmlElement(ElementName = "XTN.12", Order = 12)]
    public string SimplePhone;

    /// <summary>
    ///   The type.
    /// </summary>
    [XmlElement(ElementName = "XTN.3", Order = 3)]
    public string Type;

    #endregion
  }
}