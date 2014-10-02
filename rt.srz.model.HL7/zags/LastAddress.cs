//-----------------------------------------------------------------------
// <copyright file="LastAddress.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Адрес проживания
  /// </summary>
  [Serializable]
  public class LastAddress
  {
    /// <summary>
    /// Индекс
    /// </summary>
    [XmlElement("Индекс")]
    public string Idx { get; set; }

    /// <summary>
    /// Регион
    /// </summary>
    [XmlElement("Регион")]
    public string Region { get; set; }

    /// <summary>
    /// Район
    /// </summary>
    [XmlElement("Район")]
    public string Area { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    [XmlElement("Город")]
    public string City { get; set; }

    /// <summary>
    /// Населенный пункт
    /// </summary>
    [XmlElement("НаселенныйПункт")]
    public string Locality { get; set; }

    /// <summary>
    /// Улица
    /// </summary>
    [XmlElement("Улица")]
    public string Street { get; set; }

    /// <summary>
    /// Дом
    /// </summary>
    [XmlElement("Дом")]
    public string House { get; set; }

    /// <summary>
    /// Корпус
    /// </summary>
    [XmlElement("Корпус")]
    public string Korp { get; set; }

    /// <summary>
    /// Квартира
    /// </summary>
    [XmlElement("Квартира")]
    public string Kv { get; set; }
  }
}