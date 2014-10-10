// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Doc.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Информация о документе подтверждающем личность
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.pfr
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Информация о документе подтверждающем личность
  /// </summary>
  [Serializable]
  public class Doc
  {
    #region Public Properties

    /// <summary>
    ///   Номер документа
    /// </summary>
    [XmlElement("data_doc")]
    public string DateDoc { get; set; }

    /// <summary>
    ///   Номер документа
    /// </summary>
    [XmlElement("n_doc")]
    public string NDoc { get; set; }

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

    #endregion
  }
}