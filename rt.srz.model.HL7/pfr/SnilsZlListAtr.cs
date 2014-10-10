// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnilsZlListAtr.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Список СНИЛС от ОПФР
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.pfr
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>
  ///   Список СНИЛС от ОПФР
  /// </summary>
  [Serializable]
  [XmlRoot("snils_zl_list")]
  public class SnilsZlListAtr
  {
    #region Public Properties

    /// <summary>
    ///   Список СНИЛС
    /// </summary>
    [XmlElement("snils")]
    public List<string> Snilses { get; set; }

    /// <summary>
    ///   Заголовок сообщения
    /// </summary>
    [XmlElement("zglv")]
    public ZglvAtr Zglv { get; set; }

    #endregion
  }
}