// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZSG.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   ZSG
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.HL7.card.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   ZSG
  /// </summary>
  [Serializable]
  public class ZSG
  {
    #region Public Properties

    /// <summary>
    ///   ZSG1
    /// </summary>
    [XmlElement(ElementName = "ZSG.1", Order = 1)]
    public ZSG1 ZSG1 { get; set; }

    #endregion
  }
}