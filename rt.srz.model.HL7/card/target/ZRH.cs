// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZRH.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ZRH
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.HL7.person.target;

  #endregion

  /// <summary>
  ///   ZRH
  /// </summary>
  [Serializable]
  public class ZRH
  {
    #region Public Properties

    /// <summary>
    ///   ZRH1
    /// </summary>
    [XmlElement("ZRH.1", Order = 1)]
    public EiStructure ZRH1 { get; set; }

    /// <summary>
    ///   ZRH2
    /// </summary>
    [XmlElement("ZRH.2", Order = 2)]
    public string ZRH2 { get; set; }

    /// <summary>
    ///   ZRH3
    /// </summary>
    [XmlElement("ZRH.3", DataType = "date")]
    public DateTime ZRH3 { get; set; }

    #endregion
  }
}