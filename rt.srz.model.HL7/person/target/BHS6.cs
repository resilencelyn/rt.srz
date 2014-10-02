﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BHS6.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The bh s 6.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The bh s 6.
  /// </summary>
  [Serializable]
  public class BHS6
  {
    #region Fields

    /// <summary>
    ///   The foms code.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string FomsCode;

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