// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CONFLICTLIST.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The conflict list type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.smo
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Runtime.Serialization;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The conflict list type.
  /// </summary>
  [Serializable]
  [XmlRoot("CONFLICTLIST", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
  [DataContract]
  public class ConflictListType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the conflicts.
    /// </summary>
    [XmlElement("CONFLICT", Order = 5)]
    [DataMember]
    public List<ConflictType> Conflicts { get; set; }

    /// <summary>
    ///   Gets or sets the filename.
    /// </summary>
    [XmlElement("FILENAME", Order = 1)]
    [DataMember]
    public string Filename { get; set; }

    /// <summary>
    ///   Gets or sets the nrecords.
    /// </summary>
    [XmlElement("NRECORDS", Order = 4)]
    [DataMember]
    public int Nrecords { get; set; }

    /// <summary>
    ///   Gets or sets the przcod.
    /// </summary>
    [XmlElement("PRZCOD", Order = 3)]
    [DataMember]
    public string Przcod { get; set; }

    /// <summary>
    ///   Gets or sets the smocod.
    /// </summary>
    [XmlElement("SMOCOD", Order = 2)]
    [DataMember]
    public string Smocod { get; set; }

    /// <summary>
    ///   Gets or sets the vers.
    /// </summary>
    [XmlElement("VERS", Order = 0)]
    [DataMember]
    public string Vers { get; set; }

    #endregion
  }

  /// <summary>
  ///   The conflict type.
  /// </summary>
  [Serializable]
  [DataContract]
  public class ConflictType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the doc.
    /// </summary>
    [XmlElement("DOC", Order = 3)]
    [DataMember(Order = 4)]
    public DocType Doc { get; set; }

    /// <summary>
    ///   Gets or sets the doc curr.
    /// </summary>
    [XmlElement("DOC_CURR", Order = 6)]
    [DataMember(Order = 7)]
    public DocType DocCurr { get; set; }

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
    ///   Gets or sets the insurance curr.
    /// </summary>
    [XmlElement("INSURANCE_CURR", Order = 7)]
    [DataMember(Order = 8)]
    public InsuranceType InsuranceCurr { get; set; }

    /// <summary>
    ///   Gets or sets the person.
    /// </summary>
    [XmlElement("PERSON", Order = 2)]
    [DataMember(Order = 3)]
    public PersonType Person { get; set; }

    /// <summary>
    ///   Gets or sets the person curr.
    /// </summary>
    [XmlElement("PERSON_CURR", Order = 5)]
    [DataMember(Order = 6)]
    public PersonType PersonCurr { get; set; }

    /// <summary>
    ///   Gets or sets the reason.
    /// </summary>
    [XmlElement("REASON", Order = 1)]
    [DataMember(Order = 2)]
    public int Reason { get; set; }

    #endregion
  }
}