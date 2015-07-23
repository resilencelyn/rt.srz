// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsCompanyLicenziy.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The ins company licenziy.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.nsi.Smo
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The ins company licenziy.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class InsCompanyLicenziy
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the d_start.
    /// </summary>
    [XmlElement("d_start", Form = XmlSchemaForm.Unqualified)]
    public string DStart { get; set; }

    /// <summary>
    ///   Gets or sets the data e.
    /// </summary>
    [XmlElement("data_e", Form = XmlSchemaForm.Unqualified)]
    public string DataE { get; set; }

    /// <summary>
    ///   Gets or sets the n_doc.
    /// </summary>
    [XmlElement("n_doc", Form = XmlSchemaForm.Unqualified)]
    public string NDoc { get; set; }

    #endregion
  }
}