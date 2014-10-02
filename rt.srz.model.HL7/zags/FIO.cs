//-----------------------------------------------------------------------
// <copyright file="FIO.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// ФИО
  /// </summary>
  [Serializable]
  public class FIO
  {
    /// <summary>
    /// Фамилия
    /// </summary>
    [XmlElement("Фамилия")]
    public string Fam { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [XmlElement("Имя")]
    public string Im { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    [XmlElement("Отчество")]
    public string Ot { get; set; }
  }
}