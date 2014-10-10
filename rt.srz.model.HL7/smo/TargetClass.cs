// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TargetClass.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The address type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The address type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class AddressType
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="AddressType" /> class.
    /// </summary>
    public AddressType()
    {
      BOMG = "0";
      SUBJ = string.Empty;
      INDX = string.Empty;
      OKATO = string.Empty;
      RNNAME = string.Empty;
      NPNAME = string.Empty;
      UL = string.Empty;
      DOM = string.Empty;
      KORP = string.Empty;
      KV = string.Empty;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the bomg.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string BOMG { get; set; }

    /// <summary>
    ///   Gets or sets the c_ ads.
    /// </summary>
    [XmlElement(Order = 15)]
    [DataMember(Order = 15)]
    public string CODEKLADR { get; set; }

    /// <summary>
    ///   Gets or sets the dom.
    /// </summary>
    [XmlElement(Order = 8)]
    [DataMember(Order = 8)]
    public string DOM { get; set; }

    /// <summary>
    ///   Gets or sets the dreg.
    /// </summary>
    [XmlElement(Order = 11)]
    [DataMember(Order = 11)]
    public string DREG { get; set; }

    /// <summary>
    ///   Gets or sets the indx.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public string INDX { get; set; }

    /// <summary>
    ///   Gets or sets the korp.
    /// </summary>
    [XmlElement(Order = 9)]
    [DataMember(Order = 9)]
    public string KORP { get; set; }

    /// <summary>
    ///   Gets or sets the kv.
    /// </summary>
    [XmlElement(Order = 10)]
    [DataMember(Order = 10)]
    public string KV { get; set; }

    /// <summary>
    ///   Gets or sets the npname.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public string NPNAME { get; set; }

    /// <summary>
    ///   Gets or sets the okato.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public string OKATO { get; set; }

    /// <summary>
    ///   Gets or sets the rnname.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public string RNNAME { get; set; }

    /// <summary>
    ///   Gets or sets the subj.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string SUBJ { get; set; }

    /// <summary>
    ///   Gets or sets the ul.
    /// </summary>
    [XmlElement(Order = 7)]
    [DataMember(Order = 7)]
    public string UL { get; set; }

    #endregion
  }

  /// <summary>
  ///   The person type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class PersonType
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="PersonType" /> class.
    /// </summary>
    public PersonType()
    {
      C_OKSM = "RUS";
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the birt h_ oksm.
    /// </summary>
    [XmlElement(Order = 8)]
    [DataMember(Order = 8)]
    public string BIRTH_OKSM { get; set; }

    /// <summary>
    ///   Gets or sets the contact.
    /// </summary>
    [XmlElement(Order = 16)]
    [DataMember(Order = 16)]
    public string CONTACT { get; set; }

    /// <summary>
    ///   Gets or sets the c_ oksm.
    /// </summary>
    [XmlElement(Order = 9)]
    [DataMember(Order = 9)]
    public string C_OKSM { get; set; }

    /// <summary>
    ///   Gets or sets the ddeath.
    /// </summary>
    [XmlElement(Order = 17)]
    [DataMember(Order = 17)]
    public string DDEATH { get; set; }

    /// <summary>
    ///   Gets or sets the addres g.
    /// </summary>
    [DataMember(Name = "DOST", Order = 7)]
    [XmlElement("DOST", Order = 7)]
    public List<string> DOST { get; set; }

    /// <summary>
    ///   Gets or sets the dr.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public string DR { get; set; }

    /// <summary>
    ///   Gets or sets the email.
    /// </summary>
    [XmlElement(Order = 14)]
    [DataMember(Order = 14)]
    public string EMAIL { get; set; }

    /// <summary>
    ///   Gets or sets the fam.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string FAM { get; set; }

    /// <summary>
    ///   Gets or sets the fiopr.
    /// </summary>
    [XmlElement(Order = 15)]
    [DataMember(Order = 15)]
    public string FIOPR { get; set; }

    /// <summary>
    ///   Gets or sets the im.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string IM { get; set; }

    /// <summary>
    /// Gets or sets the kateg.
    /// </summary>
    [XmlElement(Order = 11)]
    [DataMember(Order = 11)]
    public string KATEG { get; set; }

    /// <summary>
    ///   Gets or sets the mr.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public string MR { get; set; }

    /// <summary>
    ///   Gets or sets the ot.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public string OT { get; set; }

    /// <summary>
    ///   Gets or sets the phone.
    /// </summary>
    [XmlElement(Order = 12)]
    [DataMember(Order = 12)]
    public string PHONE { get; set; }

    /// <summary>
    ///   Gets or sets the phon e_ work.
    /// </summary>
    [XmlElement(Order = 13)]
    [DataMember(Order = 13)]
    public string PHONE_WORK { get; set; }

    /// <summary>
    ///   Gets or sets the pr.
    /// </summary>
    [XmlElement(Order = 18)]
    [DataMember(Order = 18)]
    public AssigneeType PR { get; set; }

    /// <summary>
    ///   Gets or sets the snils.
    /// </summary>
    [XmlElement(Order = 10)]
    [DataMember(Order = 10)]
    public string SS { get; set; }

    /// <summary>
    ///   Gets or sets the w.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public string W { get; set; }

    #endregion
  }

  /// <summary>
  ///   The assignee type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class AssigneeType
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="AssigneeType" /> class.
    /// </summary>
    public AssigneeType()
    {
      FAM = string.Empty;
      IM = string.Empty;
      OT = string.Empty;
      RELATION = string.Empty;
      DOC = new DocType();
      PHONE = string.Empty;
      PHONE_WORK = string.Empty;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the doc.
    /// </summary>
    [DataMember(Order = 5)]
    public DocType DOC { get; set; }

    /// <summary>
    ///   Gets or sets the fam.
    /// </summary>
    [DataMember(Order = 1)]
    public string FAM { get; set; }

    /// <summary>
    ///   Gets or sets the im.
    /// </summary>
    [DataMember(Order = 2)]
    public string IM { get; set; }

    /// <summary>
    ///   Gets or sets the ot.
    /// </summary>
    [DataMember(Order = 3)]
    public string OT { get; set; }

    /// <summary>
    ///   Gets or sets the phone.
    /// </summary>
    [DataMember(Order = 6)]
    public string PHONE { get; set; }

    /// <summary>
    ///   Gets or sets the phon e_ work.
    /// </summary>
    [DataMember(Order = 7)]
    public string PHONE_WORK { get; set; }

    /// <summary>
    ///   Gets or sets the relation.
    /// </summary>
    [DataMember(Order = 4)]
    public string RELATION { get; set; }

    #endregion
  }

  /// <summary>
  ///   The doc type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class DocType
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="DocType" /> class.
    /// </summary>
    public DocType()
    {
      DOCTYPE = ((int)Doc_type_Enum.Item1).ToString(CultureInfo.InvariantCulture);
      DOCSER = string.Empty;
      DOCNUM = string.Empty;
      NAME_VP = string.Empty;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets or sets the doccat.
    /// </summary>
    [XmlElement(Order = 8)]
    [DataMember(Order = 8)]
    public string DOCCAT { get; set; }

    /// <summary>
    ///   Gets or sets the docdate.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public string DOCDATE { get; set; }

    /// <summary>
    ///   Gets or sets the docexp.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public string DOCEXP { get; set; }

    /// <summary>
    ///   Gets or sets the docnum.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public string DOCNUM { get; set; }

    /// <summary>
    ///   Gets or sets the docser.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string DOCSER { get; set; }

    /// <summary>
    ///   Gets or sets the doctype.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string DOCTYPE { get; set; }

    /// <summary>
    ///   Gets or sets the nam e_ vp.
    /// </summary>
    [XmlElement(Order = 7)]
    [DataMember(Order = 7)]
    public string NAME_VP { get; set; }

    #endregion
  }

  /// <summary>
  ///   The attachment type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class AttachmentType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the i d_ lpu.
    /// </summary>
    [DataMember(Order = 1)]
    public int ID_LPU { get; set; }

    /// <summary>
    ///   Gets or sets the i d_ ztype.
    /// </summary>
    [DataMember(Order = 2)]
    public int ID_ZTYPE { get; set; }

    #endregion
  }

  /// <summary>
  ///   The person b type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class PersonBType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the photo.
    /// </summary>
    [DataMember(Order = 2)]
    public string PHOTO { get; set; }

    /// <summary>
    ///   Gets or sets the type.
    /// </summary>
    [DataMember(Order = 1)]
    public string TYPE { get; set; }

    #endregion
  }

  /// <summary>
  ///   The order type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class OrderType
  {
    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="OrderType" /> class.
    /// </summary>
    public OrderType()
    {
      NORDER = string.Empty;
      DORDER = "1900-01-01T00:00:00";
      PRORDER = string.Empty;
    }

    #endregion

    #region Public Properties

    /// <summary>
    ///   Gets or sets the dorder.
    /// </summary>
    [DataMember(Order = 2)]
    public string DORDER { get; set; }

    /// <summary>
    ///   Gets or sets the norder.
    /// </summary>
    [DataMember(Order = 1)]
    public string NORDER { get; set; }

    /// <summary>
    ///   Gets or sets the prorder.
    /// </summary>
    [DataMember(Order = 3)]
    public string PRORDER { get; set; }

    #endregion
  }

  /// <summary>
  ///   The polis type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class PolisType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the dbeg.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public string DBEG { get; set; }

    /// <summary>
    ///   Gets or sets the dend.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public string DEND { get; set; }

    /// <summary>
    ///   Gets or sets the dstop.
    /// </summary>
    [XmlElement(Order = 7)]
    [DataMember(Order = 7)]
    public string DSTOP { get; set; }

    /// <summary>
    ///   Gets or sets the i d_ polis.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string ID_POLIS { get; set; }

    /// <summary>
    ///   Gets or sets the npolis.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public string NPOLIS { get; set; }

    /// <summary>
    ///   Gets or sets the spolis.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public string SPOLIS { get; set; }

    /// <summary>
    ///   Gets or sets the vpolis.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string VPOLIS { get; set; }

    #endregion
  }

  /// <summary>
  ///   The insurance type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class InsuranceType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the enp.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string ENP { get; set; }

    /// <summary>
    ///   Gets or sets the erp.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public int ERP { get; set; }

    /// <summary>
    ///   Gets or sets the ogrnsmo.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public string OGRNSMO { get; set; }

    /// <summary>
    ///   Gets or sets the orderz.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public OrderType ORDERZ { get; set; }

    /// <summary>
    ///   Gets or sets the polis.
    /// </summary>
    [XmlElement(ElementName = "POLIS", Order = 4)]
    [DataMember(Name = "POLIS", Order = 4)]
    public List<PolisType> POLIS { get; set; }

    /// <summary>
    ///   Gets or sets the te r_ st.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string TER_ST { get; set; }

    #endregion
  }

  /// <summary>
  ///   The vizit type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class VizitType
  {
    #region Fields

    /// <summary>
    ///   The dvizit.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string DVIZIT;

    /// <summary>
    ///   The fpolis.
    /// </summary>
    [XmlElement(Order = 6)]
    [DataMember(Order = 6)]
    public string FPOLIS;

    /// <summary>
    ///   The method.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string METHOD;

    /// <summary>
    ///   The petition.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public int PETITION;

    /// <summary>
    ///   The rpolis.
    /// </summary>
    [XmlElement(Order = 5)]
    [DataMember(Order = 5)]
    public string RPOLIS;

    /// <summary>
    ///   The rsmo.
    /// </summary>
    [XmlElement(Order = 4)]
    [DataMember(Order = 4)]
    public string RSMO;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="VizitType" /> class.
    /// </summary>
    public VizitType()
    {
      PETITION = (int)Bit.Item0;
      FPOLIS = ((int)Form_polis_Enum.П).ToString(CultureInfo.InvariantCulture);
    }

    #endregion
  }

  /// <summary>
  ///   The old person type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class OldPersonType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the dr.
    /// </summary>
    [DataMember(Order = 5)]
    public string DR { get; set; }

    /// <summary>
    ///   Gets or sets the fam.
    /// </summary>
    [DataMember(Order = 1)]
    public string FAM { get; set; }

    /// <summary>
    ///   Gets or sets the im.
    /// </summary>
    [DataMember(Order = 2)]
    public string IM { get; set; }

    /// <summary>
    ///   Gets or sets the ol d_ enp.
    /// </summary>
    [DataMember(Order = 6)]
    public string OLD_ENP { get; set; }

    /// <summary>
    ///   Gets or sets the ot.
    /// </summary>
    [DataMember(Order = 3)]
    public string OT { get; set; }

    /// <summary>
    ///   Gets or sets the tru e_ dr.
    /// </summary>
    [DataMember(Order = 7)]
    public int TRUE_DR { get; set; }

    /// <summary>
    ///   Gets or sets the w.
    /// </summary>
    [DataMember(Order = 4)]
    public int W { get; set; }

    #endregion
  }

  /// <summary>
  ///   The statement change.
  /// </summary>
  [Serializable]
  [DataContract]
  public class StatementChange
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the data.
    /// </summary>
    [XmlElement(Order = 3)]
    [DataMember(Order = 3)]
    public string DATA { get; set; }

    /// <summary>
    ///   Gets or sets the field.
    /// </summary>
    [XmlElement(Order = 2)]
    [DataMember(Order = 2)]
    public string FIELD { get; set; }

    /// <summary>
    ///   Gets or sets the version.
    /// </summary>
    [XmlElement(Order = 1)]
    [DataMember(Order = 1)]
    public string VERSION { get; set; }

    #endregion
  }

  /// <summary>
  ///   The choosing_way_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Choosing_way_Enum
  {
    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1, 

    /// <summary>
    ///   The item 2.
    /// </summary>
    [XmlEnum("2")]
    Item2, 

    /// <summary>
    ///   The item 3.
    /// </summary>
    [XmlEnum("3")]
    Item3, 

    /// <summary>
    ///   The item 4.
    /// </summary>
    [XmlEnum("4")]
    Item4, 
  }

  /// <summary>
  ///   The zone_type_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Zone_type_Enum
  {
    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1 = 1, 

    /// <summary>
    ///   The item 3.
    /// </summary>
    [XmlEnum("3")]
    Item3 = 3, 

    /// <summary>
    ///   The item 4.
    /// </summary>
    [XmlEnum("4")]
    Item4 = 4, 

    /// <summary>
    ///   The item 5.
    /// </summary>
    [XmlEnum("5")]
    Item5 = 5, 

    /// <summary>
    ///   The item 6.
    /// </summary>
    [XmlEnum("6")]
    Item6 = 6, 

    /// <summary>
    ///   The item 7.
    /// </summary>
    [XmlEnum("7")]
    Item7 = 7, 

    /// <summary>
    ///   The item 8.
    /// </summary>
    [XmlEnum("8")]
    Item8 = 8, 

    /// <summary>
    ///   The item 11.
    /// </summary>
    [XmlEnum("11")]
    Item11 = 11, 

    /// <summary>
    ///   The item 12.
    /// </summary>
    [XmlEnum("12")]
    Item12 = 12, 

    /// <summary>
    ///   The item 13.
    /// </summary>
    [XmlEnum("13")]
    Item13 = 13, 
  }

  /// <summary>
  ///   The bit.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Bit
  {
    /// <summary>
    ///   The item 0.
    /// </summary>
    [XmlEnum("0")]
    Item0, 

    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1, 
  }

  /// <summary>
  ///   The polis_type_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Polis_type_Enum
  {
    /// <summary>
    ///   The u.
    /// </summary>
    U, 

    /// <summary>
    ///   The с.
    /// </summary>
    С, 

    /// <summary>
    ///   The в.
    /// </summary>
    В, 

    /// <summary>
    ///   The п.
    /// </summary>
    П, 

    /// <summary>
    ///   The э.
    /// </summary>
    Э, 

    /// <summary>
    ///   The к.
    /// </summary>
    К, 
  }

  /// <summary>
  ///   The smo_choise_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Smo_choise_Enum
  {
    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1, 

    /// <summary>
    ///   The item 2.
    /// </summary>
    [XmlEnum("2")]
    Item2, 

    /// <summary>
    ///   The item 3.
    /// </summary>
    [XmlEnum("3")]
    Item3, 

    /// <summary>
    ///   The item 4.
    /// </summary>
    [XmlEnum("4")]
    Item4, 
  }

  /// <summary>
  ///   The true_date_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum True_date_Enum
  {
    /// <summary>
    ///   The authentically.
    /// </summary>
    [XmlEnum("1")]
    Authentically = 1, 

    /// <summary>
    ///   The ym.
    /// </summary>
    [XmlEnum("2")]
    YM, 

    /// <summary>
    ///   The y.
    /// </summary>
    [XmlEnum("3")]
    Y, 
  }

  /// <summary>
  ///   The doc_type_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Doc_type_Enum
  {
    /// <summary>
    ///   The item 0.
    /// </summary>
    [XmlEnum("0")]
    Item0 = 0, 

    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1 = 1, 

    /// <summary>
    ///   The item 2.
    /// </summary>
    [XmlEnum("2")]
    Item2, 

    /// <summary>
    ///   The item 3.
    /// </summary>
    [XmlEnum("3")]
    Item3, 

    /// <summary>
    ///   The item 4.
    /// </summary>
    [XmlEnum("4")]
    Item4, 

    /// <summary>
    ///   The item 5.
    /// </summary>
    [XmlEnum("5")]
    Item5, 

    /// <summary>
    ///   The item 6.
    /// </summary>
    [XmlEnum("6")]
    Item6, 

    /// <summary>
    ///   The item 7.
    /// </summary>
    [XmlEnum("7")]
    Item7, 

    /// <summary>
    ///   The item 8.
    /// </summary>
    [XmlEnum("8")]
    Item8, 

    /// <summary>
    ///   The item 9.
    /// </summary>
    [XmlEnum("9")]
    Item9, 

    /// <summary>
    ///   The item 10.
    /// </summary>
    [XmlEnum("10")]
    Item10, 

    /// <summary>
    ///   The item 11.
    /// </summary>
    [XmlEnum("11")]
    Item11, 

    /// <summary>
    ///   The item 12.
    /// </summary>
    [XmlEnum("12")]
    Item12, 

    /// <summary>
    ///   The item 13.
    /// </summary>
    [XmlEnum("13")]
    Item13, 

    /// <summary>
    ///   The item 14.
    /// </summary>
    [XmlEnum("14")]
    Item14, 

    /// <summary>
    ///   The item 15.
    /// </summary>
    [XmlEnum("15")]
    Item15, 

    /// <summary>
    ///   The item 16.
    /// </summary>
    [XmlEnum("16")]
    Item16, 

    /// <summary>
    ///   The item 17.
    /// </summary>
    [XmlEnum("17")]
    Item17, 

    /// <summary>
    ///   The item 18.
    /// </summary>
    [XmlEnum("18")]
    Item18, 
  }

  /// <summary>
  ///   The polis_choise_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Polis_choise_Enum
  {
    /// <summary>
    ///   The item 1.
    /// </summary>
    [XmlEnum("1")]
    Item1, 

    /// <summary>
    ///   The item 2.
    /// </summary>
    [XmlEnum("2")]
    Item2, 

    /// <summary>
    ///   The item 3.
    /// </summary>
    [XmlEnum("3")]
    Item3, 

    /// <summary>
    ///   The item 4.
    /// </summary>
    [XmlEnum("4")]
    Item4, 

    /// <summary>
    ///   The item 5.
    /// </summary>
    [XmlEnum("5")]
    Item5, 

    /// <summary>
    ///   The item 6.
    /// </summary>
    [XmlEnum("6")]
    Item6, 
  }

  /// <summary>
  ///   The form_polis_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Form_polis_Enum
  {
    /// <summary>
    ///   The п.
    /// </summary>
    П, 

    /// <summary>
    ///   The э.
    /// </summary>
    Э, 

    /// <summary>
    ///   The к.
    /// </summary>
    К, 
  }

  /// <summary>
  ///   The adress_type_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Adress_type_Enum
  {
    /// <summary>
    ///   The г.
    /// </summary>
    г, 

    /// <summary>
    ///   The о.
    /// </summary>
    о, 

    /// <summary>
    ///   The р.
    /// </summary>
    р, 

    /// <summary>
    ///   The и.
    /// </summary>
    и, 
  }

  /// <summary>
  ///   The active_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Active_Enum
  {
    /// <summary>
    ///   The active.
    /// </summary>
    [XmlEnum("0")]
    Active = 1, 

    /// <summary>
    ///   The history.
    /// </summary>
    [XmlEnum("1")]
    History = 0, 

    /// <summary>
    ///   The buffer.
    /// </summary>
    [XmlEnum("2")]
    Buffer = 2, 
  }

  /// <summary>
  ///   The sex_ enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum Sex_Enum
  {
    /// <summary>
    ///   The m.
    /// </summary>
    [XmlEnum("1")]
    M = 1, 

    /// <summary>
    ///   The j.
    /// </summary>
    [XmlEnum("2")]
    J = 2, 
  }

  /// <summary>
  ///   The operation enum.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum OperationEnum
  {
    /// <summary>
    ///   The u.
    /// </summary>
    [XmlEnum("0")]
    U = 0, 

    /// <summary>
    ///   The p 01.
    /// </summary>
    [XmlEnum("1")]
    P01 = 1, 

    /// <summary>
    ///   The p 02.
    /// </summary>
    [XmlEnum("2")]
    P02 = 2, 

    /// <summary>
    ///   The p 04.
    /// </summary>
    [XmlEnum("4")]
    P04 = 4, 

    /// <summary>
    ///   The p 06_ b c_ p.
    /// </summary>
    [XmlEnum("6")]
    P06_BC_P = 6, 

    /// <summary>
    ///   The p 03.
    /// </summary>
    [XmlEnum("7")]
    P03 = 7, 

    /// <summary>
    ///   The p 06.
    /// </summary>
    [XmlEnum("8")]
    P06 = 8, 

    /// <summary>
    ///   The p 09.
    /// </summary>
    [XmlEnum("9")]
    P09 = 9, 
  }

  /// <summary>
  ///   The ms a 1.
  /// </summary>
  [Serializable]
  [DataContract]
  public enum MSA1
  {
    /// <summary>
    ///   The aa.
    /// </summary>
    AA, 

    /// <summary>
    ///   The ae.
    /// </summary>
    AE, 

    /// <summary>
    ///   The ar.
    /// </summary>
    AR, 
  }
}