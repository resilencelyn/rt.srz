// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Identificators.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The identificators.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The identificators.
  /// </summary>
  [Serializable]
  public class Identificators
  {
    #region Fields

    /// <summary>
    ///   The identificator.
    /// </summary>
    [XmlElement(ElementName = "CX.1", Order = 1)]
    public string identificator;

    /// <summary>
    ///   The enp.
    /// </summary>
    [XmlElement(ElementName = "CX.4", Order = 4)]
    public Enp enp;

    /// <summary>
    ///   The identificator type.
    /// </summary>
    [XmlElement(ElementName = "CX.5", Order = 5)]
    public string identificatorType;

    /// <summary>
    ///   The identificator type.
    /// </summary>
    [XmlElement(ElementName = "CX.7", Order = 7)]
    public string identificatorFrom;

    /// <summary>
    ///   The identificator type.
    /// </summary>
    [XmlElement(ElementName = "CX.8", Order = 8)]
    public string identificatorTo;

    /// <summary>
    ///   The identificator type name.
    /// </summary>
    [XmlElement(ElementName = "CX.10", Order = 10)]
    public string identificatorTypeName;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="Identificators" /> class.
    /// </summary>
    public Identificators()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Identificators"/> class.
    /// </summary>
    /// <param name="identificator">
    /// The identificator.
    /// </param>
    /// <param name="enp">
    /// The enp.
    /// </param>
    /// <param name="identificatorType">
    /// The identificator type.
    /// </param>
    public Identificators(string identificator, Enp enp, string identificatorType)
    {
      this.identificator = identificator;
      this.enp = enp;
      this.identificatorType = identificatorType;
    }

    #endregion
  }
}