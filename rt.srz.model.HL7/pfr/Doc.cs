// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Doc.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   Информация о документе подтверждающем личность
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#region

using System;
using System.Xml.Serialization;

#endregion

namespace rt.srz.model.HL7.pfr
{
  /// <summary>
  ///   Информация о документе подтверждающем личность
  /// </summary>
  [Serializable]
  public class Doc
  {
    /// <summary>
    ///   Тип документа
    /// </summary>
    [XmlElement("name_doc")]
    public string NameDoc { get; set; }

    /// <summary>
    ///   Серия документа
    /// </summary>
    [XmlElement("s_doc")]
    public string SDoc { get; set; }

    /// <summary>
    ///   Номер документа
    /// </summary>
    [XmlElement("n_doc")]
    public string NDoc { get; set; }

    /// <summary>
    ///   Номер документа
    /// </summary>
    [XmlElement("data_doc")]
    public string DateDoc { get; set; }
  }
}