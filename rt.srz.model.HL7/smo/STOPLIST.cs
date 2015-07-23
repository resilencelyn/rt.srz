// --------------------------------------------------------------------------------------------------------------------
// <copyright file="STOPLIST.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The stop list type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The stop list type.
  /// </summary>
  [Serializable]
  [XmlRoot("STOPLIST", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
  [DataContract]
  public class StopListType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the filename.
    /// </summary>
    [XmlElement("FILENAME", Order = 1)]
    [DataMember(Order = 2)]
    public string Filename { get; set; }

    /// <summary>
    ///   Gets or sets the nrecords.
    /// </summary>
    [XmlElement("NRECORDS", Order = 4)]
    [DataMember(Order = 5)]
    public int Nrecords { get; set; }

    /// <summary>
    ///   Gets or sets the przcod.
    /// </summary>
    [XmlElement("PRZCOD", Order = 3)]
    [DataMember(Order = 4)]
    public string Przcod { get; set; }

    /// <summary>
    ///   Gets or sets the smocod.
    /// </summary>
    [XmlElement("SMOCOD", Order = 2)]
    [DataMember(Order = 3)]
    public string Smocod { get; set; }

    /// <summary>
    ///   Gets or sets the stop.
    /// </summary>
    [XmlElement("STOP", Order = 5)]
    [DataMember(Order = 6)]
    public List<StopType> Stop { get; set; }

    /// <summary>
    ///   Gets or sets the vers.
    /// </summary>
    [XmlElement("VERS", Order = 0)]
    [DataMember(Order = 1)]
    public string Vers { get; set; }

    #endregion
  }

  /// <summary>
  ///   The stop type.
  /// </summary>
  [Serializable]
  [DataContract]
  [XmlRoot("STOP", IsNullable = false)]
  public class StopType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the doc.
    /// </summary>
    [XmlElement("DOC", Order = 6)]
    [DataMember(Order = 7)]
    public DocType Doc { get; set; }

    /// <summary>
    ///   Gets or sets the doc curr.
    /// </summary>
    [XmlElement("DOC_CURR", Order = 8)]
    [DataMember(Order = 9)]
    public DocType DocCurr { get; set; }

    /// <summary>
    ///   Gets or sets the enp.
    /// </summary>
    [XmlElement("ENP", Order = 2)]
    [DataMember(Order = 3)]
    public string Enp { get; set; }

    /// <summary>
    ///   Gets or sets the id.
    /// </summary>
    [XmlElement("ID", Order = 0)]
    [DataMember(Order = 1)]
    public Guid Id { get; set; }

    /// <summary>
    ///   Gets or sets the insurance.
    /// </summary>
    [XmlElement("INSURANCE", Order = 4)]
    [DataMember(Order = 5)]
    public InsuranceType Insurance { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement("PERSON", Order = 5)]
    [DataMember(Order = 6)]
    public PersonType Person { get; set; }

    /// <summary>
    ///   Gets or sets the person curr.
    /// </summary>
    [XmlElement("PERSON_CURR", Order = 7)]
    [DataMember(Order = 8)]
    public PersonType PersonCurr { get; set; }

    /// <summary>
    ///   Gets or sets the polis.
    /// </summary>
    [XmlElement("POLIS", Order = 3)]
    [DataMember(Order = 4)]
    public PolisType Polis { get; set; }

    /// <summary>
    ///   Gets or sets the reason.
    /// </summary>
    [XmlElement("REASON", Order = 1)]
    [DataMember(Order = 2)]
    public int Reason { get; set; }

    #endregion
  }

  /// <summary>
  ///   Причины приостановления страхования
  /// </summary>
  public enum ReasonStop
  {
    /// <summary>
    ///   Смерть застрахованного
    /// </summary>
    Death = 1, 

    /// <summary>
    ///   Ежегодная замена страховой компании застрахованным лицом
    /// </summary>
    ChangeSmoOneYars = 2, 

    /// <summary>
    ///   Замена страховой компании по причине изменения места жительства,
    /// </summary>
    ChangeSmoChangeAddres = 3, 

    /// <summary>
    ///   Выдача временного свидетельства в другой СМО,
    /// </summary>
    OutVs = 4, 

    /// <summary>
    ///   Выявление дубликата
    /// </summary>
    Dublicates = 5, 

    /// <summary>
    ///   Прочие причины.
    /// </summary>
    Other = 6
  }
}