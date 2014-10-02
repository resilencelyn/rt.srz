// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MedCompanyDocLicPic.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The packet med company doc lic pic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.Mo
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company doc lic pic.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class MedCompanyDocLicPic
  {
    #region Public Properties

    /// <summary>
    ///   The pic copy.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string PicCopy { get; set; }

    /// <summary>
    ///   The pic page.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string PicPage { get; set; }

    #endregion
  }
}