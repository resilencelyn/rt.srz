// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZlListDop.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Данные работающего не найденные в РЗГ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.pfr
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  ///   Данные работающего не найденные в РЗГ
  /// </summary>
  [Serializable]
  [XmlRoot("zl_list")]
  public class ZlListDop
  {
    #region Public Properties

    /// <summary>
    ///   Заголовок сообщения
    /// </summary>
    [XmlElement("zglv")]
    public ZglvAtr Zglv { get; set; }

    /// <summary>
    ///   Данные застрахованного лица
    /// </summary>
    [XmlElement("zl")]
    public List<ZlDop> Zl { get; set; }

    #endregion
  }
}