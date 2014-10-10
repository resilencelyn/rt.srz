// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zglv.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Заголовок
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Заголовок
  /// </summary>
  [Serializable]
  public class Zglv
  {
    #region Public Properties

    /// <summary>
    ///   Количество записей
    /// </summary>
    [XmlElement("КоличествоЗаписей")]
    public string CountRecord { get; set; }

    /// <summary>
    ///   Дата составления
    /// </summary>
    [XmlElement("ДатаСоставления")]
    public MR DateCreate { get; set; }

    /// <summary>
    ///   Номер в пачке
    /// </summary>
    [XmlElement("ИмяФайла")]
    public string FileName { get; set; }

    /// <summary>
    ///   ФИО Руководителя
    /// </summary>
    [XmlElement("ФИОРуководителя")]
    public FIO FioDirector { get; set; }

    /// <summary>
    ///   Номер отдела
    /// </summary>
    [XmlElement("НомерОтдела")]
    public string NumDep { get; set; }

    /// <summary>
    ///   Отчетный период
    /// </summary>
    [XmlElement("ОтчетныйПериод")]
    public string Period { get; set; }

    /// <summary>
    ///   Источник данных
    /// </summary>
    [XmlElement("ИсточникДанных")]
    public string Source { get; set; }

    /// <summary>
    ///   Орган ЗАГС
    /// </summary>
    [XmlElement("ВерсияФормата")]
    public string Version { get; set; }

    #endregion
  }
}