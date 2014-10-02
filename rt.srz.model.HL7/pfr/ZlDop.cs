// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZlDop.cs" company="Rintech">
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
  public class ZlDop
  {
    /// <summary>
    ///   Порядковый номер записи
    /// </summary>
    [XmlAttribute("nomer_z")]
    public string NomerZ { get; set; }

    /// <summary>
    ///   СНИЛС
    /// </summary>
    [XmlAttribute("snils")]
    public string Snils { get; set; }

    /// <summary>
    ///   Фамилия
    /// </summary>
    [XmlAttribute("fam")]
    public string Fam { get; set; }

    /// <summary>
    ///   Имя
    /// </summary>
    [XmlAttribute("im")]
    public string Im { get; set; }

    /// <summary>
    ///   Отчество
    /// </summary>
    [XmlAttribute("ot")]
    public string Ot { get; set; }

    /// <summary>
    ///   Дата рождения
    /// </summary>
    [XmlAttribute("dr")]
    public string Dr { get; set; }

    /// <summary>
    ///   Пол
    /// </summary>
    [XmlAttribute("w")]
    public string W { get; set; }
  }
}