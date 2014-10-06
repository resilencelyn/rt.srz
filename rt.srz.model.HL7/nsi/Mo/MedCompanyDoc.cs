// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedCompanyDoc.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet med company doc.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.Mo
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company doc.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class MedCompanyDoc
  {
    #region Public Properties

    /// <summary>
    ///   The d_start.
    /// </summary>
    [XmlElement("d_start", Form = XmlSchemaForm.Unqualified)]
    public string DStart { get; set; }

    /// <summary>
    ///   The d_term.
    /// </summary>
    [XmlElement("d_term", Form = XmlSchemaForm.Unqualified)]
    public string DTerm { get; set; }

    /// <summary>
    ///   The data_e.
    /// </summary>
    [XmlElement("data_e", Form = XmlSchemaForm.Unqualified)]
    public string DataE { get; set; }

    /// <summary>
    ///   The lic pic.
    /// </summary>
    [XmlElement("licPic", Form = XmlSchemaForm.Unqualified)]
    public MedCompanyDocLicPic[] LicPic { get; set; }

    /// <summary>
    ///   The mp.
    /// </summary>
    [XmlElement("mp", Form = XmlSchemaForm.Unqualified, IsNullable = true)]
    public MedCompanyDocMp[] Mp { get; set; }

    /// <summary>
    ///   The n_doc.
    /// </summary>
    [XmlElement("n_doc", Form = XmlSchemaForm.Unqualified)]
    public string NDoc { get; set; }

    #endregion
  }
}