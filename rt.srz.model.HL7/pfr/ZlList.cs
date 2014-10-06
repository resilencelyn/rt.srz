// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZlList.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Данные работающего не найденные в РЗГ
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.pfr
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  ///   Данные работающего не найденные в РЗГ
  /// </summary>
  [Serializable]
  [XmlRoot("zl_list")]
  public class ZlList
  {
    #region Public Properties

    /// <summary>
    ///   Заголовок сообщения
    /// </summary>
    [XmlElement("zglv")]
    public Zglv Zglv { get; set; }

    /// <summary>
    ///   Данные застрахованного лица
    /// </summary>
    [XmlElement("zl")]
    public List<Zl> Zl { get; set; }

    #endregion
  }
}