// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FIO.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ФИО
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   ФИО
  /// </summary>
  [Serializable]
  public class FIO
  {
    #region Public Properties

    /// <summary>
    ///   Фамилия
    /// </summary>
    [XmlElement("Фамилия")]
    public string Fam { get; set; }

    /// <summary>
    ///   Имя
    /// </summary>
    [XmlElement("Имя")]
    public string Im { get; set; }

    /// <summary>
    ///   Отчество
    /// </summary>
    [XmlElement("Отчество")]
    public string Ot { get; set; }

    #endregion
  }
}