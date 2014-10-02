// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zl.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Данные застрахованного лица
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Xml.Serialization;

#endregion

namespace rt.srz.model.HL7.pfr
{
  /// <summary>
  ///   Данные застрахованного лица
  /// </summary>
  [Serializable]
  public class Zl
  {
    /// <summary>
    ///   СНИЛС
    /// </summary>
    [XmlElement("snils")]
    public string Snils { get; set; }

    /// <summary>
    ///   Признак идентификации застрахованного лица в ОПФР
    /// </summary>
    [XmlElement("pi")]
    public string Pi { get; set; }

    /// <summary>
    ///   Отметка о статусе работающего лица (1 - работающий  гражданин, 2 -неработающий)
    /// </summary>
    [XmlElement("id_zl")]
    public string IdZl { get; set; }

    /// <summary>
    ///   Фамилия
    /// </summary>
    [XmlElement("fam")]
    public string Fam { get; set; }

    /// <summary>
    ///   Имя
    /// </summary>
    [XmlElement("im")]
    public string Im { get; set; }

    /// <summary>
    ///   Отчество
    /// </summary>
    [XmlElement("ot")]
    public string Ot { get; set; }

    /// <summary>
    ///   Доставерность даты рождения
    /// </summary>
    [XmlElement("dostdr")]
    public string Dostdr { get; set; }

    /// <summary>
    ///   Дата рождения
    /// </summary>
    [XmlElement("dr")]
    public string Dr { get; set; }

    /// <summary>
    ///   Пол
    /// </summary>
    [XmlElement("w")]
    public string W { get; set; }

    /// <summary>
    ///   Место рождения
    /// </summary>
    [XmlElement("address_r")]
    public string AddressR { get; set; }

    /// <summary>
    ///   Индекс
    /// </summary>
    [XmlElement("index")]
    public string Index { get; set; }

    /// <summary>
    ///   Адрес регистрации
    /// </summary>
    [XmlElement("address_reg")]
    public string AddressReg { get; set; }

    /// <summary>
    ///   Серия документа
    /// </summary>
    [XmlElement("doc")]
    public Doc Doc { get; set; }
  }
}