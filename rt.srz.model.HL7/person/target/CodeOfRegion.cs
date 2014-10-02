﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeOfRegion.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The code of region.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The code of region.
  /// </summary>
  [Serializable]
  public class CodeOfRegion
  {
    #region Fields

    /// <summary>
    ///   The code.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string Code;

    /// <summary>
    ///   The iso.
    /// </summary>
    [XmlElement(ElementName = "HD.3", Order = 3)]
    public string Iso;

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "HD.2", Order = 2)]
    public string TableCode;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="CodeOfRegion" /> class.
    /// </summary>
    public CodeOfRegion()
    {
      TableCode = HL7Helper.TypeCode_Region5Code;
      Iso = "ISO";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeOfRegion"/> class.
    /// </summary>
    /// <param name="Code">
    /// The code.
    /// </param>
    public CodeOfRegion(string Code)
    {
      TableCode = HL7Helper.TypeCode_Region5Code;
      Iso = "ISO";
      this.Code = Code;
    }

    #endregion
  }
}