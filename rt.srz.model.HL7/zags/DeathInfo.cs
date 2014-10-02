//-----------------------------------------------------------------------
// <copyright file="DeathInfo.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Данные застрахованного лица  
  /// </summary>
  [Serializable]
  public class DeathInfo
  {
    /// <summary>
    /// Номер в пачке
    /// </summary>
    [XmlElement("НомерВпачке")]
    public string NumInPackage { get; set; }

    /// <summary>
    /// Орган ЗАГС
    /// </summary>
    [XmlElement("ОрганЗАГС")]
    public ORGZAGS OrgZags { get; set; }

    /// <summary>
    /// ФИО   
    /// </summary>
    [XmlElement("ФИО")]
    public FIO Fio { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    [XmlElement("Пол")]
    public string W { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [XmlElement("ДатаРождения")]
    public string Dr { get; set; }

    /// <summary>
    /// Место рождения
    /// </summary>
    [XmlElement("МестоРождения")]
    public MR Mr { get; set; }

    /// <summary>
    /// Дата смерти
    /// </summary>
    [XmlElement("ДатаСмерти")]
    public string DateDeath { get; set; }

    /// <summary>
    /// НомерЗаписиАкта
    /// </summary>
    [XmlElement("НомерЗаписиАкта")]
    public int NumRecord { get; set; }

    /// <summary>
    /// Дата записи акта
    /// </summary>
    [XmlElement("ДатаЗаписиАкта")]
    public string DateRecord { get; set; }

    /// <summary>
    /// ДПФС
    /// </summary>
    [XmlElement("УдостоверяющийДокумент")]
    public DOC Doc { get; set; }

    /// <summary>
    /// Последний адрес проживания
    /// </summary>
    [XmlElement("ПоследнееМестоЖительства")]
    public LastAddress LastAddressF { get; set; }
  }
}