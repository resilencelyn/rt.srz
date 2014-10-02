// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Boxes.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Класс подтвержедение подписи в изготовление полисов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.boxes
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   Класс подтвержедение подписи в изготовление полисов
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "BOXES", Namespace = "urn:hl7-org:v2xml")]
  public class Boxes
  {
    #region Public Properties

    /// <summary>
    ///   Короба
    /// </summary>
    [XmlElement(ElementName = "BOX")]
    public List<Box> BoxList { get; set; }

    #endregion
  }
}