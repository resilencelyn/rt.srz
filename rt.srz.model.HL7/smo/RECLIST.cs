// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RECLIST.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The rec list type.
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
  ///   The rec list type.
  /// </summary>
  [Serializable]
  [XmlRoot("RECLIST", Namespace = "http://tempuri.org/XMLSchema.xsd", IsNullable = false)]
  [DataContract]
  public class RECListType
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the end date.
    /// </summary>
    [XmlElement("END_DATE", Order = 5)]
    [DataMember(Name = "END_DATE", Order = 5)]
    public string EndDate { get; set; }

    /// <summary>
    ///   Gets or sets the filename.
    /// </summary>
    [XmlElement("FILENAME", Order = 1)]
    [DataMember(Name = "FILENAME", Order = 1)]
    public string Filename { get; set; }

    /// <summary>
    ///   Gets or sets the nrecords.
    /// </summary>
    [XmlElement("NRECORDS", Order = 6)]
    [DataMember(Name = "NRECORDS", Order = 6)]
    public int Nrecords { get; set; }

    /// <summary>
    ///   Gets or sets the smocod.
    /// </summary>
    [XmlElement("PDPCODE", Order = 3)]
    [DataMember(Name = "SMOCODE", Order = 3)]
    public string Pdpcod { get; set; }

    /// <summary>
    ///   Gets or sets the rec.
    /// </summary>
    [XmlElement("REC", Order = 7)]
    [DataMember(Name = "REC", Order = 7)]
    public List<RECType> REC { get; set; }

    /// <summary>
    ///   Gets or sets the smocod.
    /// </summary>
    [XmlElement("SMOCOD", Order = 2)]
    [DataMember(Name = "SMOCOD", Order = 2)]
    public string Smocod { get; set; }

    /// <summary>
    ///   Gets or sets the start date.
    /// </summary>
    [XmlElement("START_DATE", Order = 4)]
    [DataMember(Name = "START_DATE", Order = 4)]
    public string StartDate { get; set; }

    /// <summary>
    ///   Gets or sets the vers.
    /// </summary>
    [XmlElement("VERS", Order = 0)]
    [DataMember(Name = "VERS", Order = 0)]
    public string Vers { get; set; }

    #endregion
  }
}