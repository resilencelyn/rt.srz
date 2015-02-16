// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Qpd.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qpd.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qpd.
  /// </summary>
  [Serializable]
  public class Qpd
  {
    #region Fields

    /// <summary>
    ///   The code of region.
    /// </summary>
    [XmlElement(ElementName = "QPD.12", Order = 12)]
    public CodeOfRegion CodeOfRegion;

    /// <summary>
    ///   The date range.
    /// </summary>
    [XmlElement(ElementName = "QPD.14", Order = 14)]
    public DateRange DateRange;

    /// <summary>
    ///   The message name.
    /// </summary>
    [XmlElement(ElementName = "QPD.1", Order = 1)]
    public MessageName MessageName;

    /// <summary>
    ///   The period.
    /// </summary>
    [XmlElement(ElementName = "QPD.18", Order = 18)]
    public CneStructure Period = null;

    /// <summary>
    ///   The region list.
    /// </summary>
    [XmlElement(ElementName = "QPD.13", Order = 13)]
    public List<CodeOfRegion> RegionList;

    /// <summary>
    ///   The worker other.
    /// </summary>
    [XmlElement(ElementName = "QPD.17", Order = 17)]
    public string WorkerOther;

    /// <summary>
    ///   The year.
    /// </summary>
    [XmlElement(ElementName = "QPD.19", Order = 19)]
    public string Year = null;

    #endregion
  }
}