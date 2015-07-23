// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Zsg.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zsg.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The zsg.
  /// </summary>
  [Serializable]
  public class Zsg
  {
    #region Fields

    /// <summary>
    ///   The signature.
    /// </summary>
    [XmlElement(ElementName = "ZSG.1", Order = 1)]
    public string Signature;

    #endregion
  }
}