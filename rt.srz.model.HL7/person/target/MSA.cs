// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MSA.cs" company="Rintech">
//   Copyright (c) 2013. All rights reserved.
// </copyright>
// <summary>
//   The msa.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The msa.
  /// </summary>
  [Serializable]
  public class Msa
  {
    #region Public Properties

    /// <summary>
    ///   Gets or sets the code confirm.
    /// </summary>
    [XmlElement(ElementName = "MSA.1", Order = 1)]
    public string CodeConfirm { get; set; }

    /// <summary>
    ///   Gets or sets the reference.
    /// </summary>
    [XmlIgnore]
    public Guid Reference { get; set; }

    /// <summary>
    ///   Gets or sets the reference identificator.
    /// </summary>
    [XmlElement(ElementName = "MSA.2", Order = 2)]
    public string ReferenceIdentificator { get; set; }

    #endregion
  }
}