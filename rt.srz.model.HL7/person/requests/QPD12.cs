// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QPD12.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The qp d 12.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.requests
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The qp d 12.
  /// </summary>
  [Serializable]
  public class QPD12
  {
    #region Fields

    /// <summary>
    ///   The code of region.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string CodeOfRegion = null;

    /// <summary>
    ///   The iso.
    /// </summary>
    [XmlElement(ElementName = "HD.3", Order = 3)]
    public string Iso = "ISO";

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "HD.2", Order = 2)]
    public string TableCode = HL7Helper.TypeCode_Region2Code;

    #endregion
  }
}