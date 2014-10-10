// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ERR5.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The er r 5.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.target
{
  #region references

  using System;
  using System.Xml.Serialization;

  #endregion

  /// <summary>
  ///   The er r 5.
  /// </summary>
  [Serializable]
  public class ERR5
  {
    #region Fields

    /// <summary>
    ///   The message code.
    /// </summary>
    [XmlElement(ElementName = "CWE.1", Order = 1)]
    public string MessageCode;

    /// <summary>
    ///   The message description.
    /// </summary>
    [XmlElement(ElementName = "CWE.2", Order = 2)]
    public string MessageDescription;

    /// <summary>
    ///   The oid.
    /// </summary>
    [XmlElement(ElementName = "CWE.3", Order = 3)]
    public string Oid;

    /// <summary>
    ///   The version po.
    /// </summary>
    [XmlElement(ElementName = "CWE.7", Order = 7)]
    public string VersionPo;

    #endregion

    #region Constructors and Destructors

    /// <summary>
    ///   Initializes a new instance of the <see cref="ERR5" /> class.
    /// </summary>
    public ERR5()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ERR5"/> class.
    /// </summary>
    /// <param name="messageCode">
    /// The message code.
    /// </param>
    /// <param name="messageDescription">
    /// The message description.
    /// </param>
    /// <param name="oid">
    /// The oid.
    /// </param>
    /// <param name="versionPo">
    /// The version po.
    /// </param>
    public ERR5(string messageCode, string messageDescription, string oid, string versionPo)
    {
      MessageCode = messageCode;
      MessageDescription = messageDescription;
      Oid = oid;
      VersionPo = versionPo;
    }

    #endregion
  }
}