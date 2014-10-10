// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zags.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Данные ЗАГС по умершим
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person;

  /// <summary>
  ///   Данные ЗАГС по умершим
  /// </summary>
  [Serializable]
  [XmlRoot("ZAGS", Namespace = "urn:Hl7-org:v2xml", IsNullable = false)]
  public class Zags : BaseMessageTemplate
  {
    #region Public Properties

    /// <summary>
    ///   Список умерших
    /// </summary>
    [XmlElement("DEAD_LIST")]
    public List<Dead> DeadList { get; set; }

    /// <summary>
    ///   Список воскресших
    /// </summary>
    [XmlElement("RESURRECT_LIST")]
    public List<Resurrect> ResurrectList { get; set; }

    #endregion
  }
}