// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZglvAtr.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Заголовок файла
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.pfr
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Заголовок файла
  /// </summary>
  [Serializable]
  public class ZglvAtr
  {
    #region Public Properties

    /// <summary>
    ///   Код ОПФР
    /// </summary>
    [XmlAttribute("cod_pfr")]
    public string CodPfr { get; set; }

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
    ///   Количество записей
    /// </summary>
    [XmlAttribute("nrec")]
    public string Nrec { get; set; }

    /// <summary>
    ///   Версия
    /// </summary>
    [XmlAttribute("version")]
    public string Version { get; set; }

    #endregion
  }
}