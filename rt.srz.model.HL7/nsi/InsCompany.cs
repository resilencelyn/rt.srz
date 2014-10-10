// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InsCompany.cs" company="ÐóñÁÈÒåõ">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet ins company.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.nsi
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.nsi.Smo;
  using rt.srz.model.Hl7.nsi.target;

  /// <summary>
  ///   The packet ins company.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class InsCompany
  {
    #region Public Properties

    /// <summary>
    ///   The d_edit.
    /// </summary>
    [XmlElement("d_edit", Form = XmlSchemaForm.Unqualified)]
    public string DEdit { get; set; }

    /// <summary>
    ///   The e_mail.
    /// </summary>
    [XmlElement("e_mail", Form = XmlSchemaForm.Unqualified)]
    public string EMail { get; set; }

    /// <summary>
    ///   The fam_ruk.
    /// </summary>
    [XmlElement("fam_ruk", Form = XmlSchemaForm.Unqualified)]
    public string FamRuk { get; set; }

    /// <summary>
    ///   The fax.
    /// </summary>
    [XmlElement("fax", Form = XmlSchemaForm.Unqualified)]
    public string Fax { get; set; }

    /// <summary>
    ///   The im_ruk.
    /// </summary>
    [XmlElement("im_ruk", Form = XmlSchemaForm.Unqualified)]
    public string ImRuk { get; set; }

    /// <summary>
    ///   The inn.
    /// </summary>
    [XmlElement("inn", Form = XmlSchemaForm.Unqualified)]
    public string Inn { get; set; }

    /// <summary>
    ///   The ins advice.
    /// </summary>
    [XmlElement("insAdvice", Form = XmlSchemaForm.Unqualified)]
    public List<CompanyAdvice> InsAdvice { get; set; }

    /// <summary>
    ///   The ins include.
    /// </summary>
    [XmlElement("insInclude", Form = XmlSchemaForm.Unqualified)]
    public List<CompanyInclude> InsInclude { get; set; }

    /// <summary>
    ///   The jur address.
    /// </summary>
    [XmlElement("jurAddress", Form = XmlSchemaForm.Unqualified)]
    public CompanyJurAddress JurAddress { get; set; }

    /// <summary>
    ///   The kpp.
    /// </summary>
    [XmlElement("KPP", Form = XmlSchemaForm.Unqualified)]
    public string Kpp { get; set; }

    /// <summary>
    ///   The licenziy.
    /// </summary>
    [XmlElement("licenziy", Form = XmlSchemaForm.Unqualified)]
    public List<InsCompanyLicenziy> Licenziy { get; set; }

    /// <summary>
    ///   The nam_smok.
    /// </summary>
    [XmlElement("nam_smok", Form = XmlSchemaForm.Unqualified)]
    public string NamSmok { get; set; }

    /// <summary>
    ///   The nam_smop.
    /// </summary>
    [XmlElement("nam_smop", Form = XmlSchemaForm.Unqualified)]
    public string NamSmop { get; set; }

    /// <summary>
    ///   The ogrn.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Ogrn { get; set; }

    /// <summary>
    ///   The okopf.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Okopf { get; set; }

    /// <summary>
    ///   The org.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Org { get; set; }

    /// <summary>
    ///   The ot_ruk.
    /// </summary>
    [XmlElement("ot_ruk", Form = XmlSchemaForm.Unqualified)]
    public string OtRuk { get; set; }

    /// <summary>
    ///   The phone.
    /// </summary>
    [XmlElement("phone", Form = XmlSchemaForm.Unqualified)]
    public string Phone { get; set; }

    /// <summary>
    ///   The pst address.
    /// </summary>
    [XmlElement("pstAddress", Form = XmlSchemaForm.Unqualified)]
    public CompanyPstAddress PstAddress { get; set; }

    /// <summary>
    ///   The smocod.
    /// </summary>
    [XmlElement("smocod", Form = XmlSchemaForm.Unqualified)]
    public string Smocod { get; set; }

    /// <summary>
    ///   The tf_okato.
    /// </summary>
    [XmlElement("tf_okato", Form = XmlSchemaForm.Unqualified)]
    public string TfOkato { get; set; }

    /// <summary>
    ///   The www.
    /// </summary>
    [XmlElement("www", Form = XmlSchemaForm.Unqualified)]
    public string Www { get; set; }

    #endregion
  }
}