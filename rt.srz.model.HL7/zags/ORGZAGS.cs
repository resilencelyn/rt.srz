//-----------------------------------------------------------------------
// <copyright file="ORGZAGS.cs" company="SofTrust" author="IKhavkina">
//     Copyright (c) 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace rt.srz.model.HL7.zags
{
  using System;
  using System.Xml.Serialization;

  /// <summary>
  /// Организация ЗАГС
  /// </summary>
  [Serializable]
  public class ORGZAGS
  {
    /// <summary>
    /// Наименование организации   
    /// </summary>
    [XmlElement("НаименованиеОрганизации")]
    public string Name_Org { get; set; }
  }
}