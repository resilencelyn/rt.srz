// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BHS4.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The bh s 4.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The bh s 4.
  /// </summary>
  [Serializable]
  public class BHS4
  {
    #region Fields

    /// <summary>
    ///   The code of region.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string CodeOfRegion;

    /// <summary>
    ///   The iso.
    /// </summary>
    [XmlElement(ElementName = "HD.3", Order = 3)]
    public string Iso = "ISO";

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "HD.2", Order = 2)]
    public string TableCode = Hl7Helper.TypeCode_Region2Code;

    #endregion
  }
}