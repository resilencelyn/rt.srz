// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZlListDop.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Данные работающего не найденные в РЗГ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace rt.srz.model.HL7.pfr
{
  /// <summary>
  ///   Данные работающего не найденные в РЗГ
  /// </summary>
  [Serializable]
  [XmlRoot("zl_list")]
  public class ZlListDop
  {
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
  }
}