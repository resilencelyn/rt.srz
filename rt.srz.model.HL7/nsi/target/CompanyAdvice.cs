// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyAdvice.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet med company med advice.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.nsi.target
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet med company med advice.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class CompanyAdvice
  {
    #region Public Properties

    /// <summary>
    ///   The duved.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Duved { get; set; }

    /// <summary>
    ///   Gets or sets the kol_zl.
    /// </summary>
    [XmlElement("kol_zl", Form = XmlSchemaForm.Unqualified)]
    public string KolZl { get; set; }

    /// <summary>
    ///   The yea r_ work.
    /// </summary>
    [XmlElement("YEAR_WORK", Form = XmlSchemaForm.Unqualified)]
    public string YearWork { get; set; }

    #endregion
  }
}