// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Document.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The document.
  /// </summary>
  [Serializable]
  public class Document
  {
    #region Fields

    /// <summary>
    ///   The assignment.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string Assignment;

    /// <summary>
    ///   The code.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string Code;

    /// <summary>
    ///   The name.
    /// </summary>
    [XmlElement(ElementName = "CWE.9", Order = 9)]
    public string Name;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string Oid;

    #endregion
  }
}