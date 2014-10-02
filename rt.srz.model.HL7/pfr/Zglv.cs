// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zglv.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Заголовок файла
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Xml.Serialization;

#endregion

namespace rt.srz.model.HL7.pfr
{
  /// <summary>
  ///   Заголовок файла
  /// </summary>
  [Serializable]
  public class Zglv
  {
    /// <summary>
    ///   Имя файла
    /// </summary>
    [XmlElement("filename")]
    public string Filename { get; set; }

    /// <summary>
    ///   Номер файла
    /// </summary>
    [XmlElement("nfile")]
    public string Nfile { get; set; }

    /// <summary>
    ///   Версия
    /// </summary>
    [XmlElement("version")]
    public string Version { get; set; }

    /// <summary>
    ///   Код ОПФР
    /// </summary>
    [XmlElement("cod_pfr")]
    public string CodPfr { get; set; }

    /// <summary>
    ///   Количество записей
    /// </summary>
    [XmlElement("nrec")]
    public string Nrec { get; set; }
  }
}