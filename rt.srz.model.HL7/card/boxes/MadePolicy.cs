// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MadePolicy.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Изготовленый полис
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.card.boxes
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Изготовленый полис
  /// </summary>
  [Serializable]
  public class MadePolicy
  {
    #region Public Properties

    /// <summary>
    ///   Номер бланка
    /// </summary>
    [XmlElement(ElementName = "BKN")]
    public string BlankNum { get; set; }

    /// <summary>
    ///   Дата заявки
    /// </summary>
    [XmlElement(ElementName = "DOR")]
    public DateTime DateOrder { get; set; }

    /// <summary>
    ///   Дата изготовления
    /// </summary>
    [XmlElement(ElementName = "DPN")]
    public DateTime DateProduction { get; set; }

    /// <summary>
    ///   Номер полиса
    /// </summary>
    [XmlElement(ElementName = "ENP")]
    public string NumberPolic { get; set; }

    #endregion
  }
}