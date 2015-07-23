// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZpiZwi.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Работающие ЗЛ по данным ОПФР
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.messages
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   Работающие ЗЛ по данным ОПФР
  /// </summary>
  [Serializable]
  [XmlRoot("ZPI_ZWI", Namespace = "urn:hl7-org:v2xml", IsNullable = false)]
  public class ZpiZwi : BaseMessageTemplate
  {
    #region Public Properties

    /// <summary>
    ///   EVN
    /// </summary>
    [XmlElement("EVN", Order = 2)]
    public Evn Evn { get; set; }

    /// <summary>
    ///   Анкетные данные работающего застрахованного лица, не идентифицированного в РС ЕРЗ
    /// </summary>
    [XmlElement("PID", Order = 3)]
    public List<MessagePid> PidList { get; set; }

    /// <summary>
    ///   Перечень ЕНП работающих застрахованных лиц, идентифицированных в РС ЕРЗ
    /// </summary>
    [XmlElement("ZWL", Order = 5)]
    public List<Zwl> ZwlList { get; set; }

    /// <summary>
    ///   Отчётный период
    /// </summary>
    [XmlElement("ZWP", Order = 4)]
    public Zwp Zwp { get; set; }

    #endregion
  }
}