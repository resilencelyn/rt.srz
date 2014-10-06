// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyPstAddress.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The company pst address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.target
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The company pst address.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class CompanyPstAddress
  {
    #region Public Properties

    /// <summary>
    ///   The addr_f.
    /// </summary>
    [XmlElement("addr_f", Form = XmlSchemaForm.Unqualified)]
    public string AddrF { get; set; }

    /// <summary>
    ///   The index_f.
    /// </summary>
    [XmlElement("index_f", Form = XmlSchemaForm.Unqualified)]
    public string IndexF { get; set; }

    #endregion
  }
}