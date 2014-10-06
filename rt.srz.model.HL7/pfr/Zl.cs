// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zl.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Данные застрахованного лица
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.pfr
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Данные застрахованного лица
  /// </summary>
  [Serializable]
  public class Zl
  {
    #region Public Properties

    /// <summary>
    ///   Место рождения
    /// </summary>
    [XmlElement("address_r")]
    public string AddressR { get; set; }

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
    ///   Фамилия
    /// </summary>
    [XmlElement("fam")]
    public string Fam { get; set; }

    /// <summary>
    ///   Отметка о статусе работающего лица (1 - работающий  гражданин, 2 -неработающий)
    /// </summary>
    [XmlElement("id_zl")]
    public string IdZl { get; set; }

    /// <summary>
    ///   Имя
    /// </summary>
    [XmlElement("im")]
    public string Im { get; set; }

    /// <summary>
    ///   Индекс
    /// </summary>
    [XmlElement("index")]
    public string Index { get; set; }

    /// <summary>
    ///   Отчество
    /// </summary>
    [XmlElement("ot")]
    public string Ot { get; set; }

    /// <summary>
    ///   Признак идентификации застрахованного лица в ОПФР
    /// </summary>
    [XmlElement("pi")]
    public string Pi { get; set; }

    /// <summary>
    ///   СНИЛС
    /// </summary>
    [XmlElement("snils")]
    public string Snils { get; set; }

    /// <summary>
    ///   Пол
    /// </summary>
    [XmlElement("w")]
    public string W { get; set; }

    #endregion
  }
}