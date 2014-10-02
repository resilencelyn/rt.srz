// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZpiZA2.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Класс подтвержедение подписи в изготовление полисов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.za2
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.card.target;
  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   Класс подтвержедение подписи в изготовление полисов
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ZPI_ZA2", Namespace = "urn:hl7-org:v2xml")]
  public class ZpiZA2
  {
    #region Public Properties

    /// <summary>
    ///   Дескриптор сообщения
    /// </summary>
    public MSH MSH { get; set; }

    /// <summary>
    ///   Подпись
    /// </summary>
    public ZSG ZSG { get; set; }

    /// <summary>
    ///   Заявки
    /// </summary>
    [XmlElement(ElementName = "ZPI_ZA2.POLICY_ORDER_BATCH")]
    public List<PolicyOrderBatch> ZpiZA2PolicyOrderBatch { get; set; }

    #endregion
  }
}