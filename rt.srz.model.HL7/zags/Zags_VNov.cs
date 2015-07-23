// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zags_VNov.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Пакет от ЗАГС
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  ///   Пакет от ЗАГС
  /// </summary>
  [Serializable]
  [XmlRoot("ФайлТФОМС")]
  public class Zags_VNov
  {
    #region Public Properties

    /// <summary>
    ///   Сведения о смерти
    /// </summary>
    [XmlElement("СВЕДЕНИЯ_О_СМЕРТИ")]
    public List<DeathInfo> DeathInfo { get; set; }

    /// <summary>
    ///   Заголовок
    /// </summary>
    [XmlElement("ЗаголовокФайла")]
    public Zglv Zglv { get; set; }

    #endregion
  }
}