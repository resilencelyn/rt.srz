// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tfoms.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The packet tfoms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.nsi
{
  using System;
  using System.Xml.Schema;
  using System.Xml.Serialization;

  /// <summary>
  ///   The packet tfoms.
  /// </summary>
  [Serializable]
  [XmlType(AnonymousType = true)]
  public class Tfoms
  {
    #region Public Properties

    /// <summary>
    ///   The address.
    /// </summary>
    [XmlElement("address", Form = XmlSchemaForm.Unqualified)]
    public string Address { get; set; }

    /// <summary>
    ///   The d_edit.
    /// </summary>
    [XmlElement("d_edit", Form = XmlSchemaForm.Unqualified)]
    public string DEdit { get; set; }

    /// <summary>
    ///   The d_end.
    /// </summary>
    [XmlElement("d_end", Form = XmlSchemaForm.Unqualified)]
    public string DEnd { get; set; }

    /// <summary>
    ///   The e_mail.
    /// </summary>
    [XmlElement("e_mail", Form = XmlSchemaForm.Unqualified)]
    public string EMail { get; set; }

    /// <summary>
    ///   The fam_dir.
    /// </summary>
    [XmlElement("fam_dir", Form = XmlSchemaForm.Unqualified)]
    public string FamDir { get; set; }

    /// <summary>
    ///   The fax.
    /// </summary>
    [XmlElement("fax", Form = XmlSchemaForm.Unqualified)]
    public string Fax { get; set; }

    /// <summary>
    ///   The im_dir.
    /// </summary>
    [XmlElement("im_dir", Form = XmlSchemaForm.Unqualified)]
    public string ImDir { get; set; }

    /// <summary>
    ///   The index.
    /// </summary>
    [XmlElement("index", Form = XmlSchemaForm.Unqualified)]
    public string Index { get; set; }

    /// <summary>
    ///   The kf_tf.
    /// </summary>
    [XmlElement("kf_tf", Form = XmlSchemaForm.Unqualified)]
    public string KfTf { get; set; }

    /// <summary>
    ///   The name_tfk.
    /// </summary>
    [XmlElement("name_tfk", Form = XmlSchemaForm.Unqualified)]
    public string NameTfk { get; set; }

    /// <summary>
    ///   The name_tfp.
    /// </summary>
    [XmlElement("name_tfp", Form = XmlSchemaForm.Unqualified)]
    public string NameTfp { get; set; }

    /// <summary>
    ///   The ot_dir.
    /// </summary>
    [XmlElement("ot_dir", Form = XmlSchemaForm.Unqualified)]
    public string OtDir { get; set; }

    /// <summary>
    ///   The phone.
    /// </summary>
    [XmlElement("phone", Form = XmlSchemaForm.Unqualified)]
    public string Phone { get; set; }

    /// <summary>
    ///   The tf_kod.
    /// </summary>
    [XmlElement("tf_kod", Form = XmlSchemaForm.Unqualified)]
    public string TfKod { get; set; }

    /// <summary>
    ///   The tf_ogrn.
    /// </summary>
    [XmlElement("tf_ogrn", Form = XmlSchemaForm.Unqualified)]
    public string TfOgrn { get; set; }

    /// <summary>
    ///   The tf_okato.
    /// </summary>
    [XmlElement("tf_okato", Form = XmlSchemaForm.Unqualified)]
    public string TfOkato { get; set; }

    /// <summary>
    ///   The www.
    /// </summary>
    [XmlElement(Form = XmlSchemaForm.Unqualified)]
    public string Www { get; set; }

    #endregion
  }
}