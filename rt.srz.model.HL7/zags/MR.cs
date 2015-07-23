// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MR.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Место рождения
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Место рождения
  /// </summary>
  [Serializable]
  public class MR
  {
    #region Public Properties

    /// <summary>
    ///   Район рождения
    /// </summary>
    [XmlElement("РайонРождения")]
    public string AreMr { get; set; }

    /// <summary>
    ///   Город рождения
    /// </summary>
    [XmlElement("ГородРождения")]
    public string CityMr { get; set; }

    /// <summary>
    ///   Страна рождения
    /// </summary>
    [XmlElement("СтранаРождения")]
    public string CountryMr { get; set; }

    /// <summary>
    ///   Регион рождения
    /// </summary>
    [XmlElement("РегионРождения")]
    public string RegionMr { get; set; }

    /// <summary>
    ///   Тип места рождения
    /// </summary>
    [XmlElement("ТипМестаРождения")]
    public string TypeMr { get; set; }

    #endregion
  }
}