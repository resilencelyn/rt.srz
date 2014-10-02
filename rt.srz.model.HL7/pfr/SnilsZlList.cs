﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SnilsZlList.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Список СНИЛС от ОПФР
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
  ///   Список СНИЛС от ОПФР
  /// </summary>
  [Serializable]
  [XmlRoot("snils_zl_list", Namespace = "urn:hl7-org:v2xml")]
  public class SnilsZlList
  {
    /// <summary>
    ///   Заголовок сообщения
    /// </summary>
    [XmlElement("zglv")]
    public Zglv Zglv { get; set; }

    /// <summary>
    ///   Список СНИЛС
    /// </summary>
    [XmlElement("snils")]
    public List<string> Snilses { get; set; }
  }
}