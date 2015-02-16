// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZPI_ZA1.cs" company="РусБИТех">
//   Copyright (c) 2014. All rights reserved.
// </copyright>
// <summary>
//   The zp i_ z a 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace rt.srz.model.Hl7.person.messages
{
  #region references

  using System;
  using System.Xml.Serialization;

  using rt.srz.model.Hl7.person.target;

  #endregion

  /// <summary>
  ///   The zp i_ z a 1.
  /// </summary>
  [Serializable]
  [XmlRoot(ElementName = "ZPI_ZA1", Namespace = "urn:hl7-org:v2xml")]
  public class ZPI_ZA1 : BaseMessageTemplate
  {
    #region Fields

    /// <summary>
    ///   The application.
    /// </summary>
    [XmlElement(ElementName = "ZPI_ZA1.APPLICATION", Order = 3)]
    public ZPI_ZA1_APPLICATION Application = new ZPI_ZA1_APPLICATION();

    /// <summary>
    ///   The evn.
    /// </summary>
    [XmlElement(ElementName = "EVN", Order = 2)]
    public Evn Evn = new Evn();

    /// <summary>
    ///   The zsg.
    /// </summary>
    [XmlElement(ElementName = "ZSG", Order = 4)]
    public Zsg Zsg = new Zsg();

    #endregion
  }
}