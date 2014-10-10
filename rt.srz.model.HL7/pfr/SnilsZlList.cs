// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnilsZlList.cs" company="РусБИТех">
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
  [XmlRoot("snils_zl_list", Namespace = "urn:Hl7-org:v2xml")]
  public class SnilsZlList
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
    public Zglv Zglv { get; set; }

    #endregion
  }
}