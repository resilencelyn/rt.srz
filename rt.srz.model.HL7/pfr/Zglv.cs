// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zglv.cs" company="РусБИТех">
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
  public class Zglv
  {
    #region Public Properties

    /// <summary>
    ///   Код ОПФР
    /// </summary>
    [XmlElement("cod_pfr")]
    public string CodPfr { get; set; }

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
    ///   Количество записей
    /// </summary>
    [XmlElement("nrec")]
    public string Nrec { get; set; }

    /// <summary>
    ///   Версия
    /// </summary>
    [XmlElement("version")]
    public string Version { get; set; }

    #endregion
  }
}