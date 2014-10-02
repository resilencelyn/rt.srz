// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZglvAtr.cs" company="Rintech">
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
  public class ZglvAtr
  {
    /// <summary>
    ///   Имя файла
    /// </summary>
    [XmlAttribute("filename")]
    public string Filename { get; set; }

    /// <summary>
    ///   Номер файла
    /// </summary>
    [XmlAttribute("nfile")]
    public string Nfile { get; set; }

    /// <summary>
    ///   Версия
    /// </summary>
    [XmlAttribute("version")]
    public string Version { get; set; }

    /// <summary>
    ///   Код ОПФР
    /// </summary>
    [XmlAttribute("cod_pfr")]
    public string CodPfr { get; set; }

    /// <summary>
    ///   Количество записей
    /// </summary>
    [XmlAttribute("nrec")]
    public string Nrec { get; set; }
  }
}