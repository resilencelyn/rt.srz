// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZSG1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ZSG1
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   ZSG1
  /// </summary>
  [Serializable]
  public class ZSG1
  {
    #region Public Properties

    /// <summary>
    ///   Signature
    /// </summary>
    [XmlElement(ElementName = "Signature")]
    public string Signature { get; set; }

    #endregion
  }
}