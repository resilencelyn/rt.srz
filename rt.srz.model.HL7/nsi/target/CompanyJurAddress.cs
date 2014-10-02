// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyJurAddress.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The packet med company jur address.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.target
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company jur address.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class CompanyJurAddress
  {
    #region Public Properties

    /// <summary>
    ///   The addr_j.
    /// </summary>
    [XmlElement("addr_j", Form = XmlSchemaForm.Unqualified)]
    public string AddrJ { get; set; }

    /// <summary>
    ///   The index_j.
    /// </summary>
    [XmlElement("index_j", Form = XmlSchemaForm.Unqualified)]
    public string IndexJ { get; set; }

    #endregion
  }
}