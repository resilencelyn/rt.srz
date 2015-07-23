// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ORGZAGS.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   Организация ЗАГС
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  ///   Организация ЗАГС
  /// </summary>
  [Serializable]
  public class ORGZAGS
  {
    #region Public Properties

    /// <summary>
    ///   Наименование организации
    /// </summary>
    [XmlElement("НаименованиеОрганизации")]
    public string Name_Org { get; set; }

    #endregion
  }
}