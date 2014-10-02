//-----------------------------------------------------------------------
// <copyright file="Zags_VNov.cs" type="Zags_VNov" company="SofTrust">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  /// Пакет от ЗАГС
  /// </summary>
  [Serializable]
  [XmlRoot("ФайлТФОМС")]
  public class Zags_VNov
  {
    /// <summary>
    /// Заголовок
    /// </summary>
    [XmlElement("ЗаголовокФайла")]
    public Zglv Zglv { get; set; }

    /// <summary>
    /// Сведения о смерти
    /// </summary>
    [XmlElement("СВЕДЕНИЯ_О_СМЕРТИ")]
    public List<DeathInfo> DeathInfo { get; set; }
  }
}