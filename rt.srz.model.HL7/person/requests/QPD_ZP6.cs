// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QPD_ZP6.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The qp d_ z p 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The qp d_ z p 6.
  /// </summary>
  [Serializable]
  public class QPD_ZP6
  {
    #region Fields

    /// <summary>
    ///   The code of region.
    /// </summary>
    [XmlElement(ElementName = "QPD.12", Order = 12)]
    public QPD12 CodeOfRegion = new QPD12();

    /// <summary>
    ///   The message name.
    /// </summary>
    [XmlElement(ElementName = "QPD.1", Order = 1)]
    public MessageName MessageName = new MessageName();

    /// <summary>
    ///   The period code.
    /// </summary>
    [XmlElement(ElementName = "QPD.18", Order = 18)]
    public CneStructure PeriodCode = new CneStructure();

    /// <summary>
    ///   The report year.
    /// </summary>
    [XmlElement(ElementName = "QPD.19", Order = 19)]
    public string ReportYear;

    /// <summary>
    ///   The resident eployee.
    /// </summary>
    [XmlElement(ElementName = "QPD.17", Order = 17)]
    public string ResidentEployee = "Y";

    #endregion
  }
}