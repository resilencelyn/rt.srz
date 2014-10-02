//-----------------------------------------------------------------------
// <copyright file="MR.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Место рождения
  /// </summary>
  [Serializable]
  public class MR
  {
    /// <summary>
    /// Тип места рождения
    /// </summary>
    [XmlElement("ТипМестаРождения")]
    public string TypeMr { get; set; }

    /// <summary>
    /// Город рождения
    /// </summary>
    [XmlElement("ГородРождения")]
    public string CityMr { get; set; }

    /// <summary>
    /// Район рождения
    /// </summary>
    [XmlElement("РайонРождения")]
    public string AreMr { get; set; }

    /// <summary>
    /// Регион рождения
    /// </summary>
    [XmlElement("РегионРождения")]
    public string RegionMr { get; set; }

    /// <summary>
    /// Страна рождения
    /// </summary>
    [XmlElement("СтранаРождения")]
    public string CountryMr { get; set; }
  }
}