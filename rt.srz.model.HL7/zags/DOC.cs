// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DOC.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Документ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Документ
  /// </summary>
  [Serializable]
  public class DOC
  {
    #region Public Properties

    /// <summary>
    ///   Код документа
    /// </summary>
    [XmlElement("КодУдостоверяющего")]
    public string CodeDoc { get; set; }

    /// <summary>
    ///   Дата документа
    /// </summary>
    [XmlElement("ДатаВыдачи")]
    public string DateDoc { get; set; }

    /// <summary>
    ///   Кем выдан
    /// </summary>
    [XmlElement("КемВыдан")]
    public string IssuedBy { get; set; }

    /// <summary>
    ///   Номер документа
    /// </summary>
    [XmlElement("НомерУдостоверяющего")]
    public string NumDoc { get; set; }

    /// <summary>
    ///   Серия документа
    /// </summary>
    [XmlElement("Серия")]
    public string SerDoc { get; set; }

    /// <summary>
    ///   Тип документа
    /// </summary>
    [XmlElement("ТипУдостоверяющего")]
    public string TypeDoc { get; set; }

    #endregion
  }
}