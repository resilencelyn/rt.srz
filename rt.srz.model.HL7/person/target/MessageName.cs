// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageName.cs" company="Альянс">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The message name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The message name.
  /// </summary>
  [Serializable]
  public class MessageName
  {
    #region Fields

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string Name;

    /// <summary>
    ///   The name big.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string NameBig;

    /// <summary>
    ///   The table code.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string TableCode = "1.2.643.2.40.1.9";

    #endregion
  }
}