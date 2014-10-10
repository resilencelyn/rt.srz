// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyInclude.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet med company med include.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.nsi.target
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company med include.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class CompanyInclude
  {
    #region Public Properties

    /// <summary>
    ///   The d_begin.
    /// </summary>
    [XmlElement("d_begin", Form = XmlSchemaForm.Unqualified)]
    public string DBegin { get; set; }

    /// <summary>
    ///   The d_end.
    /// </summary>
    [XmlElement("d_end", Form = XmlSchemaForm.Unqualified)]
    public string DEnd { get; set; }

    /// <summary>
    ///   Gets or sets the nal_p.
    /// </summary>
    [XmlElement("Nal_p", Form = XmlSchemaForm.Unqualified)]
    public string NalP { get; set; }

    /// <summary>
    ///   The name_e.
    /// </summary>
    [XmlElement("name_e", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
    public List<CompanyIncludeNameE> NameE { get; set; }

    #endregion
  }
}