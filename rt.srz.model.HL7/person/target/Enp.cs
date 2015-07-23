// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enp.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The enp.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The enp.
  /// </summary>
  [Serializable]
  public class Enp
  {
    #region Fields

    /// <summary>
    ///   The code tfoms.
    /// </summary>
    [XmlElement(ElementName = "HD.1", Order = 1)]
    public string CodeTfoms;

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
    ///   Initializes a new instance of the <see cref="Enp" /> class.
    /// </summary>
    public Enp()
    {
      TableCode = Hl7Helper.TypeCode_Region5Code;
      Iso = "ISO";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Enp"/> class.
    /// </summary>
    /// <param name="CodeTfoms">
    /// The code tfoms.
    /// </param>
    public Enp(string CodeTfoms)
    {
      TableCode = Hl7Helper.TypeCode_Region5Code;
      Iso = "ISO";
      this.CodeTfoms = CodeTfoms;
    }

    #endregion
  }
}