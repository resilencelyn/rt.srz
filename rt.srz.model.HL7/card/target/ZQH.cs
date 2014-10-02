// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZQH.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   ZQH
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   ZQH
  /// </summary>
  [Serializable]
  public class ZQH
  {
    #region Public Properties

    /// <summary>
    ///   ZQH1
    /// </summary>
    [XmlElement("ZQH.1", Order = 1)]
    public string ZQH1 { get; set; }

    /// <summary>
    ///   ZQH3
    /// </summary>
    [XmlElement("ZQH.3", Order = 3)]
    public string ZQH3 { get; set; }

    /// <summary>
    ///   ZQH4
    /// </summary>
    [XmlElement("ZQH.4", Order = 4)]
    public string ZQH4 { get; set; }

    /// <summary>
    ///   ZQH5
    /// </summary>
    [XmlElement("ZQH.5", Order = 5)]
    public string ZQH5 { get; set; }

    /// <summary>
    ///   ZQH7
    /// </summary>
    [XmlElement("ZQH.7", Order = 7)]
    public ZQH7 ZQH7 { get; set; }

    #endregion
  }
}